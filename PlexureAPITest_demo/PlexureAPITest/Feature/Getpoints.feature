Feature: Points


 Scenario Outline: Get Points with correct token
	Given I got the token
	And   I have product id <productId>
	And   I GET points 
	Then  The respons code should be <statuscode>
	
	Examples: 
	| CaseID     | productId | statuscode |
	| ValidID    | 1         | 200        |
	| InvalideID | 10        | 400        |

	
 Scenario Outline: Get Points with incorrect token
	Given I put token as <incorrectToken>
	And   I have product id <productId>
	And   I GET points 
	And   I got the purchase points
	Then  The respons code should be <statuscode>
	

	Examples: 
	| CaseID       | incorrectToken |productId | statuscode |
	| NullToken    | 1              | 1        | 401        |
	| SringToken   | test           | 1        | 401        |
	| InvalidToken | %^#$           | 1        | 401        |
