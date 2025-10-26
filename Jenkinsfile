pipeline {
  agent any
  
  parameters {
    string(
	  name:'TEST_CATEGORY',
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
// Путь к папке с allure-результатами (предполагается, что адаптер пишет туда)
def resultsDir = "${env.WORKSPACE}\TestResults\allure-results"

// Попытка сгенерировать отчет только если есть результаты
bat """
if exist "${resultsDir}" (
echo Generating Allure report from ${resultsDir}
allure generate "${resultsDir}" --clean -o allure-report
) else (
echo WARNING: Allure results folder not found: ${resultsDir}
)
"""

// Попытка опубликовать через Allure Jenkins Plugin (если плагин установлен)
try {
allure([includeProperties: false, jdk: '', results: [[path: 'TestResults\\allure-results']], reportBuildPolicy: 'ALWAYS'])
} catch (err) {
echo "Allure publish step failed or skipped: ${err}"
}

// Архивируем артефакты
archiveArtifacts artifacts: '**/*.trx', allowEmptyArchive: true
archiveArtifacts artifacts: 'allure-report/**/*', allowEmptyArchive: true
}
}

failure { echo 'Test run is failed!' }
success { echo 'SUCCESS!!!' }
}
}		  
