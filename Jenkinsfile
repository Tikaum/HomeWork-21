pipeline {
  agent any
  
  parameters {
    string(
	  name:'TAST_CATEGORY',
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
        bat "dotnet test --filter \"Category=${params.TEST_CATEGORY}\" --logger:\"trx;LogFileName=test-result.trx\""
      }
    }	
	
	stage('Generate Allure Report'){
	  steps{
	  bat 'allure generate TestResults -- clean -o allure-report'
	  }
	}
	
	stage ('Publish report'){
	steps{
	allure(
	includeProperties: false,
	jdk: '',
	results: [(path: 'TestResults')],
	reportBuildPolicy: 'ALWAYS'
	)
	}
	}

  }
  
  post {
    always {	  
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
