﻿@LoginRequired
Feature: LanguageFeature

User performing operations on the language tab 
 
@regression
Scenario Outline: create a new language
	Given navigate to the language tab
	And click the Add New Button
	When input the  "<Language>" and "<Level>" and click the add button
	Then a "<Message>" will be display to show the result
	
Examples:
	| Language                     | Level  | Message                                               |
	| English                      | Fluent | has been added to your languages.                     |
	| Hindi                        |        | Please enter language and level                       |
	| Malayalam                    | Native | has been added to your languages.                     |
	| Malayalam                    | Native | This language is already exist in your language list. |
	| malayalam                    | Native | This language is already exist in your language list. |
	| English                      | Fluent | Duplicated data.                                      |
	|                              | Basic  | Please enter language and level                       |
	
	| 2353428&&^#*@&@##^&@@        | Basic  | has been added to your languages.                     |
	| <script><Div></div></script> | Basic  | has been added to your languages.                     |

Scenario: user cancel to add a new language
	Given navigate to the language tab
	And click on the Add New Button
	When input the  valid information and click the Cancel button
		| Language | Level |
		| Hindi    | Basic |
	Then no new language is created

Scenario Outline: user updates a language
	Given navigate to the language tab
	And click the edit Button of a "<Language>"
	When change the "<Language_name>" and "<Level>" and click Update button
	Then a "<Message>" will be display to show the result
	
Examples:
	| Language | Language_name | Level  | Message                                               |
	| Malayalam| Hindi         | Basic  | has been updated to your languages                    |
	| Malayalam| English       | Basic  | This language is already added to your language list. |
	| Malayalam| English       | Fluent | This language is already added to your language list. |

Scenario Outline: user deletes a language
	Given navigate to the language tab
	When click on the delete Button of a "<Language>"
	Then a "<Message>" will be display to show the result
	
Examples:
	| Language | Message                              |
	| English  | has been deleted from your languages |

Scenario: user not able to add new language when has reached 4 languages
	Given navigate to the language tab
	When the user has exactly 4 languages
	Then the button should not be visible
	When the user has fewer than 4 languages
	Then the button should be visible

Scenario: User adds a duplicate language
    Given navigate to the language tab
    And click the Add New Button
    When input the "Malayalam" and "Native" and click the add button
    Then "This language is already exist in your language list." will be displayed

Scenario: User adds a language with invalid characters
    Given navigate to the language tab
    And click the Add New Button
    When input the "12345!@#$%" and "Basic" and click the add button
    Then "Invalid input for language." will be displayed

Scenario: User adds a language without selecting a level
    Given navigate to the language tab
    And click the Add New Button
    When input the "English" and leave the level empty
    Then "Please select a level." will be displayed

Scenario: User adds an extremely long language input
    Given navigate to the language tab
    And click the Add New Button
    When input the "<A string of 1000+ characters>" and "Basic" and click the add button
    Then "Input exceeds the maximum character limit." will be displayed

Scenario: User attempts SQL injection in language field
    Given navigate to the language tab
    And click the Add New Button
    When input the "'; DROP TABLE Languages; --" and "Fluent" and click the add button
    Then "Invalid input for language." will be displayed
