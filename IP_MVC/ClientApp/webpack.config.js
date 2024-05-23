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
        flip: './src/ts/Views/Project/flip.ts',
        createFlow: './src/ts/Views/Flow/createFlow.ts',
        createProject: './src/ts/Views/Project/createProject.ts',
        createScroll: './src/ts/Views/Flow/createScroll.ts',
        controlQuestions: './src/ts/Views/Flow/Questions/controlQuestions.ts',
        EditQuestion: './src/ts/Views/Question/editQuestionPresenter.ts'
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