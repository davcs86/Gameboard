"use strict";

var path = require("path");
var webpack = require("webpack");
var autoprefixer = require("autoprefixer");
var isUndefined = require("lodash").isUndefined;

var ExtractTextPlugin = require("extract-text-webpack-plugin");

var rootPublic = path.resolve("./App");

module.exports = function(minify, runEslint) {

    minify = isUndefined(minify) ? false : minify;
    runEslint = isUndefined(runEslint) ? true : runEslint;

    const ExtractSass = new ExtractTextPlugin({
        filename: `css/[name]${minify ? ".min" : ""}.css`,
        allChunks: true
    });

    const webpackConfig = {
        // entry points
        entry: {
            app: __dirname + "/App/setup/setup.bootstrap.js"
        },
        devtool: "source-map",
        // output system
        output: {
            path: __dirname + "/wwwroot",
            filename: `js/[name]${minify ? ".min" : ""}.js`,
            publicPath: "/"
        },
        // resolves modules
        resolve: {
            extensions: [".js", ".es6", ".scss", ".css"],
            alias: {
                modernizr$: path.resolve(__dirname, ".modernizrrc") 
            }
        },
        module: {
            rules: [
                {
                    loader: 'webpack-modernizr?useConfigFile',
                    test: /\.modernizrrc$/
                },
                {
                    test: /\.(js|es6)$/,
                    exclude: /(node_modules|wwwroot)/,
                    enforce: "pre",
                    use: [{ loader: "eslint-loader" }]
                }, {
                    test: /\.html$/,
                    loaders: [
                        {
                            loader: "html-loader",
                            query: {
                                attrs: ["img:src", "img:data-src"]
                            }
                        }
                    ]
                }, {
                    test: /\.(scss|sass)$/,
                    loader: ExtractSass.extract({
                        fallback: "style-loader",
                        use: [
                            {
                                loader: "css-loader",
                                options: {
                                    sourceMap: true,
                                    root: rootPublic,
                                    minimize: minify
                                }
                            },
                            { loader: "postcss-loader" },
                            {
                                loader: "sass-loader",
                                options: {
                                    includePaths: [
                                        rootPublic
                                    ],
                                    sourceMap: true,
                                    outputStyle: "expanded",
                                    sourceMapContents: true
                                }
                            }
                        ]
                    })
                }, {
                    test: /\.(js|es6)$/,
                    exclude: /(node_modules|wwwroot)/,
                    loaders: [
                        {
                            loader: "babel-loader",
                            query: {
                                cacheDirectory: false
                            }
                        }
                    ]
                }, {
                    test: /\.css$/,
                    loaders: [
                        "style-loader",
                        "css-loader?sourceMap"+(minify?"&minimize":""),
                        "postcss-loader"
                    ]
                }, {
                    test: /\.(woff2|woff|ttf|eot|svg)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                    loaders: [
                        {
                            loader: "url-loader",
                            query: {
                                name: "assets/fonts/[name]_[hash].[ext]"
                            }
                        }
                    ]
                }, {
                    test: /\.(jpe?g|png|gif)$/i,
                    loaders: [
                        {
                            loader: "url-loader",
                            query: {
                                name: "assets/images/[name]_[hash].[ext]",
                                limit: 10000
                            }
                        }
                    ]
                }
            ]
        },
        plugins: [
            new webpack.LoaderOptionsPlugin({
                options: {
                    context: __dirname,
                    eslint: {
                        emitError: true,
                        failOnError: true
                    },
                    // PostCSS
                    postcss: [autoprefixer({ browsers: ["last 5 versions"] })]
                }
            }),
            new webpack.ProvidePlugin({
                $: "jquery",
                jQuery: "jquery",
                'window.jQuery': "jquery",
                'window.jquery': "jquery",

                moment: "moment",
                'window.moment': "moment",

                _: "lodash",
                'window._': "lodash",

            }),
            //new webpack.NoErrorsPlugin(),
            new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
            new webpack.optimize.AggressiveMergingPlugin({
                moveToParents: true
            }),
            new webpack.optimize.CommonsChunkPlugin({
                name: "common",
                async: true,
                children: true,
                minChunks: Infinity
            }),
            ExtractSass,
            new webpack.optimize.UglifyJsPlugin({
                minimize: true,
                warnings: false,
                sourceMap: true,
                mangle: false
            })
        ]
    };
    // remove eslint
    if (!runEslint) {
        webpackConfig.module.rules.unshift();
    }
    // remove uglify
    if (!minify) {
        webpackConfig.plugins.pop();
    }
    return webpackConfig;
};