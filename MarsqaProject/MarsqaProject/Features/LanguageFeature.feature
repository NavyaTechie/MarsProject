﻿@LoginRequired
Feature: user can operate a language



#@regression
#Scenario Outline: create a new language
#	Given navigate to the language tab
#	And click the Add New Button
#	When input the  "<language>" and "<level>" and click the add button
#	Then a "<message>" will be display to show the result
#	
#Examples:
#	| language                     | level  | message                                               |
#	| Mandarin                     | Basic  | has been added to your languages.                     |
#	| mandarin                     |        | Please enter language and level                       |
#	| English                      | Fluent | has been added to your languages.                     |
#	| English                      | Fluent | This language is already exist in your language list. |
#	| english                      | Fluent | This language is already exist in your language list. |
#	| English                      | Basic  | Duplicated data.                                      |
#	|                              | Basic  | Please enter language and level                       |
#	
#	| 348517&^#*@&@##^&@@&         | Basic  | has been added to your languages.                     |
#	| <script><Div></div></script> | Basic  | has been added to your languages.                     |
#
#Scenario: user cancel to create a new language
#	Given navigate to the language tab
#	And click the Add New Button
#	When input the  valid information and click the Cancel button
#		| language | level |
#		| Mandarin | Basic |
#	Then no more language is created
#
#Scenario Outline: user can update a language
#	Given navigate to the language tab
#	And click the edit Button of a "<language>"
#	When change the "<language_name>" and "<level>" and click Update button
#	Then a "<message>" will be display to show the result
#	
#Examples:
#	| language | language_name | level  | message                                               |
#	| Mandarin | mandarin      | Basic  | has been updated to your languages                    |
#	| Mandarin | English       | Basic  | This language is already added to your language list. |
#	| Mandarin | English       | Fluent | This language is already added to your language list. |
#
#Scenario Outline: user can delete a language
#	Given navigate to the language tab
#	When click the delete Button of a "<language>"
#	Then a "<message>" will be display to show the result
#	
#Examples:
#	| language | message                              |
#	| English  | has been deleted from your languages |
#
#Scenario: user can see the AddNew button when language number less than 4
#	Given navigate to the language tab
#	When the user has 4 languages
#	Then the button should not be visible
#	When user delete a language
#	Then the button should be visible


Background:
	Given User login the system
	When The data is clean up  
	And Navigate to the language tab

@Regression
Scenario: TC_001_View the language list with 4 records
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
Scenario: TC_001_View the language list with 3 records
	Given Add a language succeed
		| language | level        |
		| Java  | Basic     |
		| C#    | Fluent       |
		| JS    | Native/Bilingual |
	When I click the language tab
	Then I should see the language list with correct information
	And The AndNew button is visible

@Positive @Regression
Scenario Outline: TC_002_Create a language with valid value
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language | level    |
	| Java  | Fluent |

@Regression
Scenario Outline: TC_003_Cancel to create a new language with valid value
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language cancel button
	Then No more language is created
Examples:
	| language | level    |
	| Java  | Fluent |

@Negative
Scenario Outline: TC_004_Create new language with empty value
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	Then No more language is created
Examples:
	| language | level              |
	|       | Fluent           |
	| Java  | Choose Language Level |

@Negative
Scenario: TC_005_Duplicate language Name with same level
	Given Add a language succeed
		| language | level    |
		| Java  | Fluent |
	When Add another language
		| language | level    |
		| Java  | Fluent |
	Then No more language is created

@Negative
Scenario: TC_006_Skill name Case Sensitivity
	Given Add a language succeed
		| language | level    |
		| Java  | Fluent |
	When Add another language
		| language | level    |
		| java  | Fluent |
	Then No more language is created

@Negative @Destructive
Scenario Outline: TC_007_Create a new language with special symbols
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language               | level    |
	| $#&%&*&* \\;'.khkjh | Fluent |


@Negative @Destructive
Scenario Outline: TC_008_SQL Injection in language name
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language           | level    |
	| "' OR 1=1; -- " | Fluent |

@Negative @Destructive
Scenario: TC_009_Script Injection in Language name
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language                           | level    |
	| "<script>alert('XSS')</script>" | Fluent |


@Negative @Destructive
Scenario: TC_010_Extremely long Language name
	Given Click the language AddNew Button
	When Input the language name "<language>" and level "<level>"
	And Click the language Add button
	Then New language is created
Examples:
	| language                   | level    |
	| (10,000 'a' characters) | Fluent |

@Regression
Scenario Outline: TC_011_Delete a language
	Given Add a language succeed
		| language | level    |
		| Java  | Fluent |
	When Click the language delete icon
	Then The language will be delete


@Regression
Scenario: TC_012_Update a language with new name
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
Scenario: TC_013_Cancel to update a language
	Given Add a language succeed
		| language | level    |
		| Java  | Fluent |
	When Click the language edit icon
	And Update the language
		| language | level  |
		| C#    | Fluent |
	And Click the language cancel button
	Then The language is not updated
