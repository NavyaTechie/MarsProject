﻿@LoginRequired
Feature: user can operate a skill

Background:
	Given The data is clean up  and  Navigate to the skill tab

@Positive @Regression
Scenario Outline: Create a skill with valid value
	Given click the AddNew Button
When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then a skill is created
Examples:
	| skill | level    |
	| Java  | Beginner |

@Regression
Scenario Outline: Cancel to create a new skill with valid value
	Given click the AddNew Button
When Input the skill name "<skill>" and level "<level>"
	And click the cancel button
	Then no skill is created
Examples:
	| skill | level    |
	| Java  | Beginner |

@Negative
Scenario Outline: Create new skill with empty value
	Given click the AddNew Button
When Input the skill name "<skill>" and level "<level>"
	Then no skill is created
Examples:
	| skill | level              |
	|       | Beginner           |
	| Java  | Choose Skill Level |

@Negative
Scenario: Duplicate skillName with same level
	Given add a skill succeed
		| skill | level    |
		| Java  | Beginner |
	When add another skill
		| skill | level    |
		| Java  | Beginner |
	Then no more skill is created

@Negative
Scenario: Skill name Case Sensitivity
	Given add a skill succeed
		| skill | level    |
		| Java  | Beginner |
	When add another skill
		| skill | level    |
		| java  | Beginner |
	Then no more skill is created

@Negative @Destructive
Scenario Outline: Create a new skill with special symbols
	Given click the AddNew Button
	When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then skill is created
Examples:
	| skill               | level    |
	| $#&%&*&* \\;'.khkjh | Beginner |


@Negative @Destructive
Scenario Outline: SQL Injection in skill name
	Given click the AddNew Button
	When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then skill is created
Examples:
	| skill           | level    |
	| "' OR 1=1; -- " | Beginner |

@Negative @Destructive
Scenario: Script Injection in Skill name
	Given click the AddNew Button
	When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then skill is created
Examples:
	| skill                           | level    |
	| "<script>alert('XSS')</script>" | Beginner |


@Negative @Destructive
Scenario: Extremely long Language name
	Given click the AddNew Button
	When Input the skill name "<skill>" and level "<level>"
	And Click the Add button
	Then skill is created
Examples:
	| skill                   | level    |
	| (10,000 'a' characters) | Beginner |

@Regression
Scenario Outline: Delete a skill
	Given add a skill succeed
		| skill | level    |
		| Java  | Beginner |
	When click the delete icon
	Then the skill will be delete


@Regression
Scenario: Update a skill with new name
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
Scenario: Cancel to update a skill
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
Scenario: View the skill list
	Given add a skill succeed
		| skill | level        |
		| Java  | Beginner     |
		| C#    | Expert       |
		| JS    | Intermediate |
	When I click the skill tab
	Then I should see the skill list

