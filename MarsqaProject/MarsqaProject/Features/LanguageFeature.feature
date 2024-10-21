﻿@LoginRequired
Feature: user can operate a language





Background:
	Given User login the system
	When The data is clean up  
	And Navigate to the language tab

@Regression
Scenario: View the language list with 4 records
	Given Add a language succeed
		| language | level        |
		| Java  | Basic     |
		| C#    | Fluent       |
		| JS    | Native/Bilingual |
		| Mandarin    | Native/Bilingual |
	When I click the language tab
	Then I should see the language list with correct information
	And The AndNew button is invisible

@Regression
Scenario: View the language list with 3 records
	Given Add a language succeed
		| language | level        |
		| Java  | Basic     |
		| C#    | Fluent       |
		| JS    | Native/Bilingual |
	When I click the language tab
	Then I should see the language list with correct information
	And The AndNew button is visible

@Positive @Regression
Scenario Outline: Create a language with valid value
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language | level    |
	| Java  | Fluent |

@Regression
Scenario Outline: Cancel to create a new language with valid value
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language cancel button
	Then No more language is created
Examples:
	| language | level    |
	| Java  | Fluent |

@Negative
Scenario Outline: Create new language with empty value
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	Then No more language is created
Examples:
	| language | level              |
	|       | Fluent           |
	| Java  | Choose Language Level |

@Negative
Scenario: Duplicate language Name with same level
	Given Add a language succeed
		| language | level    |
		| Java  | Fluent |
	When Add another language
		| language | level    |
		| Java  | Fluent |
	Then No more language is created

@Negative
Scenario: Skill name Case Sensitivity
	Given Add a language succeed
		| language | level    |
		| Java  | Fluent |
	When Add another language
		| language | level    |
		| java  | Fluent |
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
Scenario Outline: SQL Injection in language name
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
Scenario: Extremely long Language name
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language                   | level    |
	| 3,000 'n' characters | Fluent |

@Regression
Scenario Outline: Delete a language
	Given Add a language succeed
		| language | level    |
		| Java  | Fluent |
	When Click the language delete icon
	Then The language will be delete


@Regression
Scenario: Update a language with new name
	Given Add a language succeed
		| language | level    |
		| Java  | Fluent |
	When Click the language edit icon
	And Update the language
		| language | level  |
		| C#    | Fluent |
	And Click language update button
	Then The language is updated

@Regression
Scenario: Cancel to update a language
	Given Add a language succeed
		| language | level    |
		| Java  | Fluent |
	When Click the language edit icon
	And Update the language
		| language | level  |
		| C#    | Fluent |
	And Click the language cancel button
	Then The language is not updated
