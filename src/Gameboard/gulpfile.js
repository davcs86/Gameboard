
var gulp = require("gulp");
var gutil = require("gulp-util");
var webpack = require("webpack");
var webpackConfig = require("./webpack.config.js");

var webpackCB = function(cb) {
    return function(err, stats) {
        if (err) throw new gutil.PluginError("webpack", err);
        gutil.log("[webpack]", stats.toString());
        cb();
    };
};

gulp.task("default", function (callback) {
    // run webpack
    webpack(webpackConfig(true, true), webpackCB(callback));
    webpack(webpackConfig(false, true), webpackCB(callback));
});

gulp.task("no-lint", function (callback) {
    // run webpack w/ linting
    webpack(webpackConfig(true, false), webpackCB(callback));
    webpack(webpackConfig(false, false), webpackCB(callback));
});