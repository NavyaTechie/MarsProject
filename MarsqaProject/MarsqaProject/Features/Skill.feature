Feature: Skill

User performing operations on the skill tab

@regression
Scenario Outline: create a new skill
	Given navigate to the skills tab
	And click the AddNew Button
	When input the "<skill>" and "<level>" and click the add button
	Then a "<message>" will be displayed to show the result
	
Examples:
	| skill                        | level        | message                                  |
	| C++                          | Intermediate | has been added to your skills            |
	| C++                          | Beginner     | Duplicated data                          |
	|                              | Beginner     | Please enter skill and experience level. |
	| C#                           |              | Please enter skill and experience level. |
	| C++                          | Expert       | has been added to your skills            |
	| Java                         | Beginner     | has been added to your skills            |
	| <script><Div></div></script> | Intermediate | has been added to your skills            |
	| /9874562&^#*%$^(*&@&@##^&@@& | Intermediate | has been added to your skills            |
	|                              | Expert       | Please enter skill and experience level  |


Scenario Outline: user cancel to create a new skill
	Given navigate to the skills tab
	And click the AddNew Button
	When input the "<skill>" and "<level>" and click the cancel button
	Then no more skill is created
Examples:
	| skill    | level    |
	| C++      | Beginner |

Scenario Outline: user can update a skill
	Given navigate to the skills tab
	And click the edit button of a "<skill>"
	When change the "<skill_name>""<level>" and click Update button
	Then a "<message>" will be displayed to show the result
Examples:
	| skill   | skillname  | level        | message                                         |
	| C#      | Python     | Expert       | has been updated to your skills                 |
	| Java    | Python     | Expert       | is already added to your skill list            |
	| C++     | C#         | Intermediate | This skill is already added to your skill list |

Scenario Outline: user can delete a skill
	Given navigate to the skills tab
	When click the delete button of a "<skill>"
	Then a "<message>" will be displayed to show the result
	
Examples:
	| skill | message          |
	| C#    | has been deleted |