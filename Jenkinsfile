pipeline {
  agent any

  parameters {
    string(
      name: 'TEST_CATEGORY',
      defaultValue: 'AllTests',
      description: 'Run test with category'
    )
  }

  stages {
    stage('Clean') {
      steps {
        script {
          deleteDir()
        }
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
        echo "Select test category: ${params.TEST_CATEGORY}"
        bat "dotnet test --filter \"Category=${params.TEST_CATEGORY}\" --logger:\"trx;LogFileName=TestResults\\test-result.trx\""
      }
    }
  }

  post {
    always {
      script {
        // Путь к папке с allure-результатами (используем прямые слэши)
        def resultsDir = "${env.WORKSPACE}/TestResults/allure-results"

        // Генерация отчёта (с проверкой exit-кода)
        def allureGenerated = bat(
          script: """
            if exist "${resultsDir}" (
              echo Generating Allure report from "${resultsDir}"
              allure generate "${resultsDir}" --clean -o allure-report
            ) else (
              echo WARNING: Allure results folder not found: "${resultsDir}"
              exit 0
            )
          """,
          returnStatus: true
        )

        if (allureGenerated != 0) {
          echo "Failed to generate Allure report!"
        }

        // Публикация отчёта (если плагин установлен)
        try {
          allure([
            includeProperties: false,
            jdk: '',
            results: [[path: 'TestResults/allure-results']],
            reportBuildPolicy: 'ALWAYS'
          ])
        } catch (err) {
          echo "Allure publish step failed or skipped: ${err}"
        }

        // Архивируем артефакты
        archiveArtifacts artifacts: '**/*.trx', allowEmptyArchive: true
        archiveArtifacts artifacts: 'allure-report/**/*', allowEmptyArchive: true
      }
    }

    failure {
      echo 'Test run is failed!'
    }

    success {
      echo 'SUCCESS!!!'
    }
  }
}