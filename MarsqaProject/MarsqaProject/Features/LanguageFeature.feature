﻿@LoginRequired
Feature: user can operate a language


Background:
	Given User is logged in the system
	When The data is clean up  
	And Navigate to the language tab

@Positive @Regression
Scenario Outline: Create a language with valid value
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language | level    |
	| English  | Fluent |

@Regression
Scenario: Viewing list with 4 records
	Given Add a language succeed
		| language | level         |
		| Hindi  | Conversational  |
		| English   | Fluent           |
		| Malayalam    | Native/Bilingual |
		| Tamil      | Basic |
	When I click the language tab
	Then I should see the language list with correct information
	And The AndNew button is invisible

@Regression
Scenario Outline: Cancel to create a new language with valid value
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language cancel button
	Then No more language is created
Examples:
	| language | level    |
	| English  | Fluent |

@Negative
Scenario Outline: Create new language with empty value
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	Then No more language is created
Examples:
	| language | level              |
	|       | Fluent                |
	| Hindi  | Choose Language Level |

@Negative
Scenario: Duplicate language Name with same level
	Given Add a language succeed
		| language | level    |
		| English  | Fluent |
	When Add another language
		| language | level    |
		| English  | Fluent |
	Then No more language is created

@Negative
Scenario: Skill name Case Sensitivity
	Given Add a language succeed
		| language | level    |
		| English  | Fluent |
	When Add another language
		| language | level    |
		| english  | Fluent |
	Then No more language is created

@Negative @Destructive
Scenario Outline: Create a new language with special symbols
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language               | level    |
	| $#&%&*&* \\;'.khkjh | Fluent |


@Negative @Destructive
Scenario Outline: Update language with SQL Injection in name
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language           | level    |
	| "' OR 1=1; -- " | Fluent |

@Negative @Destructive
Scenario: Script Injection in Language name
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language                           | level    |
	| "<script>alert('XSS')</script>" | Fluent |


@Negative @Destructive
Scenario: Update language with huge payloads 
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language                   | level    |
	| 3,000 'n' characters | Fluent |

@Regression
Scenario Outline: Delete a language from list
	Given Add a language succeed
		| language | level    |
		| English  | Fluent |
	When Click the language delete icon
	Then The language will be delete


@Regression
Scenario: Updating a language with new name
	Given Add a language succeed
		| language | level    |
		| English  | Fluent |
	When Click the language edit icon
	And Update the language
		| language | level  |
		| French    | Fluent |
	And Click language update button
	Then The language is updated

@Regression
Scenario: Cancelling to update a language
	Given Add a language succeed
		| language | level    |
		| English  | Fluent |
	When Click the language edit icon
	And Update the language
		| language | level  |
		| French    | Fluent |
	And Click the language cancel button
	Then The language is not updated
