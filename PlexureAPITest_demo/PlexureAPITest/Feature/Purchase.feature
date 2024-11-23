Feature: Purchase


 Scenario Outline: Purchase happy path
	Given I have token <token>
	And   I have product id <productId>
	And   I POST purchase
	Then  The purchase respons code should be <statuscode>
	Then  The purchase resonpse key should have values
	| key       | value               |
	| message   | Purchase completed. |
	| points    | 100                 |
	Examples: 
	| CaseID  | token                                  | productId | statuscode |
	| ValidID | 37cb9e58-99db-423c-9da5-42d5627614c5   | 1         | 202        |


Scenario Outline: Purchase unhappy path
	Given I have token <token>
	And   I have product id <productId>
	And   I POST purchase
	Then  The purchase respons code should be <statuscode>
	
	Examples: 

	| CaseID           | token                                | productId | statuscode |
	| InvalideID       | 37cb9e58-99db-423c-9da5-42d5627614c5 | 10        | 400        |
	| ProductIDNull    | 37cb9e58-99db-423c-9da5-42d5627614c5 |           | 500        |
	| ProductIDIllegal | 37cb9e58-99db-423c-9da5-42d5627614c5 | &$^       | 500        |
	| InvalideToken    | 37cb9e58-99db-423c-9da5-42d5627617c5 | 1         | 401        |
	| TokenNull        |                                      | 1         | 401        |
	| TokenIllegal     | *&^%$#####                           | 1         | 401        |
	
	