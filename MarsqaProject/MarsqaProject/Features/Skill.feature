﻿@LoginRequired
Feature: user can operate a skill

#@regression
#$Scenario Outline: create a new skill
#	Given navigate to the skills tab
#	And click the AddNew Button
#	When input the "<skill>" and "<level>" and click the add button
#	Then a "<message>" will be displayed to show the result
	
#Examples:
#	| skill                        | level        | message                                 |
#	| Postman                      |              | Please enter skill and experience level |
#	| java                         | Beginner     | has been added to your skills          |
#	| Java                         | Beginner     | has been added to your skills          |
#	| Java                         | Beginner     | Duplicated data                        |
#	| C++                          | Expert       | has been added to your skills          |
#	| C#                           | Beginner     | has been added to your skills          |
#	| <script><Div></div></script> | Intermediate | has been added to your skills          |
#	| /348517&^#*%$^(*&@&@##^&@@&  | Intermediate | has been added to your skills          |
#	|                              | Expert       | Please enter skill and experience level |

# 
# Scenario Outline: user cancel to create a new skill
# 	Given navigate to the skills tab
# 	And click the AddNew Button
# 	When input the "<skill>" and "<level>" and click the cancel button
# 	Then no more skill is created
# Examples:
# 	| skill    | level    |
# 	| testing2 | Beginner |
# 
# #Scenario Outline: user can update a skill
#	Given navigate to the skills tab
#	And click the edit button of a "<skill>"
#	When change the "<skill_name>""<level>" and click Update button
#	Then a "<message>" will be displayed to show the result
#Examples:
#	| skill   | skill_name | level        | message                                         |
#	| java    | java222    | Expert       | has been updated to your skills                 |
#	| postman | java       | Expert       | is already added to your skill list            |
#	| postman | Jmeter     | Intermediate | This skill is already added to your skill list |
#
#Scenario Outline: user can delete a skill
#	Given navigate to the skills tab
#	When click the delete button of a "<skill>"
#	Then a "<message>" will be displayed to show the result
#	
#Examples:
#	| skill | message          |
#	| Java  | has been deleted |
#

Background:
	Given The data is clean up  and  Navigate to the skill tab

@Positive @Regression
Scenario Outline: TC_002_Create a skill with valid value
	Given click the AddNew Button
When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then a skill is created
Examples:
	| skill | level    |
	| Java  | Beginner |

@Regression
Scenario Outline: TC_003_Cancel to create a new skill with valid value
	Given click the AddNew Button
When Input the skill name "<skill>" and level "<level>"
	And click the cancel button
	Then no skill is created
Examples:
	| skill | level    |
	| Java  | Beginner |

@Negative
Scenario Outline: TC_004_Create new skill with empty value
	Given click the AddNew Button
When Input the skill name "<skill>" and level "<level>"
	Then no skill is created
Examples:
	| skill | level              |
	|       | Beginner           |
	| Java  | Choose Skill Level |

@Negative
Scenario: TC_005_Duplicate skillName with same level
	Given add a skill succeed
		| skill | level    |
		| Java  | Beginner |
	When add another skill
		| skill | level    |
		| Java  | Beginner |
	Then no more skill is created

@Negative
Scenario: TC_006_Skill name Case Sensitivity
	Given add a skill succeed
		| skill | level    |
		| Java  | Beginner |
	When add another skill
		| skill | level    |
		| java  | Beginner |
	Then no more skill is created

@Negative @Destructive
Scenario Outline: TC_007_Create a new skill with special symbols
	Given click the AddNew Button
	When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then skill is created
Examples:
	| skill               | level    |
	| $#&%&*&* \\;'.khkjh | Beginner |


@Negative @Destructive
Scenario Outline: TC_008_SQL Injection in skill name
	Given click the AddNew Button
	When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then skill is created
Examples:
	| skill           | level    |
	| "' OR 1=1; -- " | Beginner |

@Negative @Destructive
Scenario: TC_009_Script Injection in Skill name
	Given click the AddNew Button
	When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then skill is created
Examples:
	| skill                           | level    |
	| "<script>alert('XSS')</script>" | Beginner |


@Negative @Destructive
Scenario: TC_010_Extremely long Language name
	Given click the AddNew Button
	When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then skill is created
Examples:
	| skill                   | level    |
	| (10,000 'a' characters) | Beginner |

@Regression
Scenario Outline: TC_011_Delete a skill
	Given add a skill succeed
		| skill | level    |
		| Java  | Beginner |
	When click the delete icon
	Then the skill will be delete


@Regression
Scenario: TC_012_Update a skill with new name
	Given add a skill succeed
		| skill | level    |
		| Java  | Beginner |
	When click the edit icon
	And update the skill with below data
		| skill | level  |
		| C#    | Expert |
	And click update button
	Then the skill is updated

@Regression
Scenario: TC_013_Cancel to update a skill
	Given add a skill succeed
		| skill | level    |
		| Java  | Beginner |
	When click the edit icon
	And update the skill with below data
		| skill | level  |
		| C#    | Expert |
	And click the cancel button
	Then the skill is not updated

@Regression
Scenario: TC_001_View the skill list
	Given add a skill succeed
		| skill | level        |
		| Java  | Beginner     |
		| C#    | Expert       |
		| JS    | Intermediate |
	When I click the skill tab
	Then I should see the skill list

