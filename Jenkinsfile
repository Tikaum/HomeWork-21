pipeline {
    agent any

    parameters {
        string(name: 'TEST_CATEGORY', defaultValue: 'AllTests', description: 'Run test with category')
    }

    environment {
        ALLURE_RESULTS_DIR = 'TestResults/allure-results'
        ALLURE_REPORT_DIR = 'allure-report'
    }

    stages {
        stage('Clean') {
            steps {
                deleteDir()
            }
        }

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                echo "Running tests with category: ${params.TEST_CATEGORY}"
                // Запуск тестов с генерацией .trx и allure-результатов
                bat "dotnet test --filter \"Category=${params.TEST_CATEGORY}\" --logger:\"trx;LogFileName=TestResults/test-result.trx\""
            }
        }
    }

    post {
        always {
            script {
                // Архивируем .trx отчёты
                archiveArtifacts artifacts: 'TestResults/*.trx', allowEmptyArchive: true

                // Проверяем, что папка с allure-результатами существует
                def allureResultsExist = bat(
                    script: "if exist ${env.ALLURE_RESULTS_DIR} (echo true) else (echo false)",
                    returnStdout: true
                ).trim()

                if (allureResultsExist == 'true') {
                    echo "Generating Allure report..."
                    // Генерация Allure отчёта
                    bat "allure generate ${env.ALLURE_RESULTS_DIR} --clean -o ${env.ALLURE_REPORT_DIR}"

                    // Архивируем Allure отчёт
                    archiveArtifacts artifacts: "${env.ALLURE_REPORT_DIR}/**/*", allowEmptyArchive: true
                } else {
                    echo "Allure results directory not found: ${env.ALLURE_RESULTS_DIR}"
                }
            }
        }

        failure {
            echo 'Tests failed!'
        }

        success {
            echo 'Tests passed!'
        }
    }
}