const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;

const merge = require('webpack-merge');
const treeShakableModules = [
    './Client/polyfills.ts',
    '@angular/animations',
    '@angular/common',
    '@angular/compiler',
    '@angular/core',
    '@angular/flex-layout',
    '@angular/forms',    
    '@angular/http',
    '@angular/material',
    '@angular/platform-browser-dynamic',
    '@angular/platform-browser',
    '@angular/platform-browser/animations',
    '@angular/router',
    '@markpieszak/ng-application-insights',
    '@ngrx/effects',
    '@ngrx/entity',
    '@ngrx/router-store',
    '@ngrx/store-devtools',
    '@ngrx/store',
    '@ngx-translate/core',
    'angular2-jwt',
    'eventsource-polyfill',
    'gettext-parser',
    'hammerjs',
    'iconv-lite',
    'ng-click-outside',
    'ng2-avatar',
    'ng2-responsive',
    'readable-stream',
    'url-join',
    'web-animations-js',
    'webpack-material-design-icons',
    'zone.js',
];
const nonTreeShakableModules = [
    // 'bootstrap',
    // 'bootstrap/dist/css/bootstrap.css',
    'core-js',
    // 'es6-promise',
    // 'es6-shim',
    // 'event-source-polyfill',
    // 'jquery',
];
const allModules = treeShakableModules.concat(nonTreeShakableModules);

module.exports = (env) => {
  console.log(`env = ${JSON.stringify(env)}`)
  console.log(`building vendor bundle`);    
    const isDevBuild = !(env && env.prod);
    const sharedConfig = {
        stats: { modules: false },
        resolve: { extensions: [ '.js' ] },
        module: {
            rules: [
                { test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' }
            ]
        },
        output: {
            publicPath: 'dist/',
            filename: '[name].js',
            library: '[name]_[hash]'
        },
        plugins: [
            // new BundleAnalyzerPlugin(),
            // new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery' }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
            new webpack.ContextReplacementPlugin(/\@angular\b.*\b(bundles|linker)/, path.join(__dirname, './Client')), // Workaround for https://github.com/angular/angular/issues/11580
            new webpack.ContextReplacementPlugin(/(.+)?angular(\\|\/)core(.+)?/, path.join(__dirname, './Client')), // Workaround for https://github.com/angular/angular/issues/14898
            new webpack.IgnorePlugin(/^vertx$/) // Workaround for https://github.com/stefanpenner/es6-promise/issues/100
        ]
    };

    const clientBundleConfig = merge(sharedConfig, {
        entry: {
            // To keep development builds fast, include all vendor dependencies in the vendor bundle.
            // But for production builds, leave the tree-shakable ones out so the AOT compiler can produce a smaller bundle.
            vendor: isDevBuild ? allModules : nonTreeShakableModules
        },
        output: { path: path.join(__dirname, 'wwwroot', 'dist') },
        module: {
            rules: [
                { test: /\.css(\?|$)/, use: ExtractTextPlugin.extract({ use: isDevBuild ? 'css-loader' : 'css-loader?minimize' }) }
            ]
        },
        plugins: [      
            new ExtractTextPlugin('vendor.css'),
            new webpack.DllPlugin({
                path: path.join(__dirname, 'wwwroot', 'dist', '[name]-manifest.json'),
                name: '[name]_[hash]'
            })
        ].concat(isDevBuild ? [] : [
            new webpack.optimize.UglifyJsPlugin()
        ])
    });

    return [clientBundleConfig];
}
