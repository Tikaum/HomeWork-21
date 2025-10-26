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
script { deleteDir() }
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
// Сохраняем .trx в папке TestResults.
// Важно: чтобы появились allure-результаты, ваш тестовый адаптер должен писать в TestResults\allure-results
bat "dotnet test --filter \"Category=${params.TEST_CATEGORY}\" --logger:\"trx;LogFileName=TestResults\\test-result.trx\""
}
}

stage('Generate Allure Report') {
steps {
// Генерация отчёта из папки с allure-результатами
bat 'allure generate TestResults\\allure-results --clean -o allure-report'
}
}

stage('Publish report') {
steps {
allure(
includeProperties: false,
jdk: '',
results: [[path: 'TestResults\\allure-results']],
reportBuildPolicy: 'ALWAYS'
)
}
}
}

post {
always {
archiveArtifacts artifacts: '/*.trx', allowEmptyArchive: true
archiveArtifacts artifacts: 'allure-report//*', allowEmptyArchive: true
}
failure {
echo 'Test run is failed!'
}
success {
echo 'SUCCESS!!!'
}
}
}