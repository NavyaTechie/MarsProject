Feature: LoginFeature

As a registered user 
I would like to sign in to the project mars application
So that I can access my profile

@regression 

  Scenario: login with valid Credentials
	Given navigates to the login page
	When enter valid credentials 
    And  I click on the login button
	Then should be redirected to the profile page

 Scenario Outline: login with invalid credentials
  Given  navigates to the login page
    When I enter an invalid email "navya.rem@ggmail.com"
    And  I enter an invalid password "55Password54"
    And  I click on the login button
    Then I should see an error message "Invalid credentials"
