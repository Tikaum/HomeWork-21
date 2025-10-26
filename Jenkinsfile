pipeline {
    agent any

    parameters {
        string(name: 'TEST_CATEGORY', defaultValue: 'AllTests', description: 'Run test with category')
    }

    environment {
        ALLURE_RESULTS_DIR = 'TestResults/TestResults/allure-results'
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
                bat "dotnet test --filter \"Category=${params.TEST_CATEGORY}\" --logger:\"trx;LogFileName=test-result.trx\""
            }
        }
    }

  post {
    always {
	  bat 'allure generate TestResults --clean -o allure-report'
	  script {
	    allure([
      includeProperties: false,
      jdk: '',
      results: [[path: 'TestResults']],
      reportBuildPolicy: 'ALWAYS'
    ])
	  }
	  archiveArtifacts artifacts: '**/*.trx', allowEmptyArchive: true
	}
    failure {
      echo 'Test run is failed!'
    }
    success {
      echo 'SUCCESS!!!'
    }
  }
}