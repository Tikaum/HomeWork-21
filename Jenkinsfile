pipeline {
    agent any

    parameters {
        string(name: 'TEST_CATEGORY', defaultValue: 'AllTests', description: 'Run test with category')
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
                // Сохраняем .trx в папку TestResults
                bat "dotnet test --filter \"Category=${params.TEST_CATEGORY}\" --logger:\"trx;LogFileName=TestResults\\test-result.trx\""
            }
        }
    }

    post {
        always {
            // Архивируем .trx отчёты
            archiveArtifacts artifacts: 'TestResults/*.trx', allowEmptyArchive: true
        }

        failure {
            echo 'Tests failed!'
        }

        success {
            echo 'Tests passed!'
        }
    }
}