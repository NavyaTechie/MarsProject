Feature: LoginFeature

As a registered user
I want to log in to the application
So that i can view my personalized profile

@regression

Scenario: login with valid Credentials
	Given navigates to the login page
	When enter valid credentials and click the login button
	Then should be redirected to the profile page


