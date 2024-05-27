const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");


module.exports = {
    entry: {
        site: './src/ts/site.ts',
        validation: './src/ts/validation.ts',
        index: './src/ts/index.ts',
        range: './src/ts/Views/Flow/Questions/range.ts',
        redirectedQuestionId: './src/ts/Views/Flow/Questions/redirectedQuestionId.ts',
        goingToNextQuestionCircularFlow: './src/ts/Views/Flow/goingToNextQuestionCircularFlow.ts',
        showPopUp: './src/ts/showPopUp.ts',
        createScroll: './src/ts/Views/Flow/createScroll.ts',
        analytics: './src/ts/Analytics/analytics.ts',
        controlQuestions: './src/ts/Views/Flow/Questions/controlQuestions.ts',
        editQuestion: './src/ts/Views/Question/editQuestionPresenter.ts',
        editProject: './src/ts/Views/Project/projectPresenter.ts',
        editFlow: './src/ts/Views/Flow/flowPresenter.ts',
        createQuestion: './src/ts/Views/Question/createQuestionPresenter.ts',
        manageFacilitators: './src/ts/Views/Project/manageFacilitators.ts',
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