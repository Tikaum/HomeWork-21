pipeline {
  agent any
  
  parameters {
    choice(
	  name:'TEST_CATEGORY',
	  choices:['exended', 'smoke', 'regression'],
	  description: 'Set category'
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
