Feature: Homepage search function

  As a user, I want to search for skills on the market, so that I can view resources matching my skills and languages.

Scenario Outline: Display seller's details matching entered search string
	Given I navigate to the homepage
	When I enter a skill "<skill>" into the search bar
	And I click the search button
	Then I should see a list of services matching the entered skill
	When I click the "<seller>" infomation and I have logged in the system
	Then The system should display the seller's details

Examples:
	| skill     | seller        |
	| QA Tester | Sangeetha Narayan    |


#Scenario Outline: Display the service details matching entered search string
#	Given I navigate to the homepage
#	When I enter a skill "<skill>" into the search bar
#	And I click the search button
#	Then I should see a list of services matching the entered skill
#	When I click the "<service>" infomation
#	Then The system should display the details
#Examples:
#	| skill     | service        |
#	| QA Tester | API Testing    | 
