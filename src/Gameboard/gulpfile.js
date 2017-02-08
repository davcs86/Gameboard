
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

gulp.task("build:min:eslint", function (callback) {
    // run webpack w/ minify & w/ lint
    webpack(webpackConfig(true, true), webpackCB(callback));
});

gulp.task("build:nomin:eslint", function (callback) {
    // run webpack w/o minify & w/ lint
    webpack(webpackConfig(false, true), webpackCB(callback));
});

gulp.task("build:min:noeslint", function (callback) {
    // run webpack w/ minify & w/o lint
    webpack(webpackConfig(true, false), webpackCB(callback));
});

gulp.task("build:nomin:noeslint", function (callback) {
    // run webpack w/o minify & w/o lint
    webpack(webpackConfig(false, false), webpackCB(callback));
});

gulp.task("default", ["default:lint"]);
gulp.task("default:lint", ["build:min:eslint", "build:nomin:eslint"]);
gulp.task("default:no-lint", ["build:min:noeslint", "build:nomin:noeslint"]);
