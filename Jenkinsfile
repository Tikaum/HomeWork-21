pipeline {
  agent any

  parameters {
    string(name: 'TEST_CATEGORY', defaultValue: 'AllTests', description: 'Run test with category')
  }

  stages {
    stage('Clean') { steps { deleteDir() } }
    stage('Checkout') { steps { checkout scm } }
    stage('Restore') { steps { bat 'dotnet restore' } }
    stage('Build') { steps { bat 'dotnet build --configuration Release' } }

    stage('Test') {
      steps {
        echo "Running tests with category: ${params.TEST_CATEGORY}"
        // Сохраняем .trx в TestResults
        bat "dotnet test --filter \"Category=${params.TEST_CATEGORY}\" --logger:\"trx;LogFileName=TestResults\\test-result.trx\""
      }
    }
  }

  post {
    always {
      script {
        // Конвертация .trx → allure-results (если нет адаптера)
        bat "trx2allure TestResults\\test-result.trx TestResults\\allure-results"

        // Генерация отчёта
        def resultsDir = "${env.WORKSPACE}\\TestResults\\allure-results"
        bat """
          if exist "${resultsDir}" (
            echo Generating Allure report from "${resultsDir}"
            allure generate "${resultsDir}" --clean -o allure-report
          ) else (
            echo ERROR: Allure results not found at "${resultsDir}"
          )
        """

        // Публикация отчёта
        try {
          allure([
            includeProperties: false,
            jdk: '',
            results: [[path: 'TestResults\\allure-results']],
            reportBuildPolicy: 'ALWAYS'
          ])
        } catch (err) {
          echo "Allure publish failed: ${err}"
        }

        // Архивируем артефакты
        archiveArtifacts artifacts: '**/*.trx', allowEmptyArchive: true
        archiveArtifacts artifacts: 'allure-report/**/*', allowEmptyArchive: true
      }
    }

    failure { echo 'Tests failed!' }
    success { echo 'Tests passed!' }
  }
}