Feature: Purchase


 Scenario Outline: Purchase  
	Given I got the token
	And   I have product id <productId>
	And   I POST purchase
	And   I got the purchase points
	Then  The respons code should be <statuscode>
	
	Examples: 
	| CaseID      | productId | statuscode |
	| ValidID     |     1     |  200       |
	| InvalideID  |     10    |  400       |
	