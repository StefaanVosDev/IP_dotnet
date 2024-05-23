const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");


module.exports = {
    entry: {
        site: './src/ts/site.ts',
        validation: './src/ts/validation.ts',
        index: './src/ts/index.ts',
        range: './src/ts/Flow/Questions/range.ts',
        redirectedQuestionId: './src/ts/Flow/Questions/redirectedQuestionId.ts',
        goingToNextQuestionCircularFlow: './src/ts/Flow/goingToNextQuestionCircularFlow.ts',
        flip: './src/ts/Project/flip.ts',
        analytics: './src/ts/Analytics/analytics.ts',
        createFlow: './src/ts/Flow/createFlow.ts',
        createScroll: './src/ts/Flow/createScroll.ts',
        controlQuestions: './src/ts/Flow/Questions/controlQuestions.ts',
        editQuestion: './src/ts/Question/edit.ts',
    },
    output: {
        filename: '[name].entry.js',
        path: path.resolve(__dirname, '..', 'wwwroot', 'dist'),
        clean: true
    },
    devtool: 'source-map',
    mode: 'development',
    resolve: {
      extensions: [".ts", ".js"],
      extensionAlias: {'.js': ['.js', '.ts']}
    },
    module: {
        rules: [
            {
                test: /\.ts$/i,
                use: ['ts-loader'],
                exclude: /node_modules/
            },
            {
                test: /\.s?css$/,
                use: [{loader: MiniCssExtractPlugin.loader}, 'css-loader', 'sass-loader']
            },
            {
                test: /\.(png|svg|jpg|jpeg|gif|webp)$/i,
                type: 'asset'
            },
            {
                test: /\.(eot|woff(2)?|ttf|otf|svg)$/i,
                type: 'asset'
            }
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: "[name].css"
        })
    ]
};