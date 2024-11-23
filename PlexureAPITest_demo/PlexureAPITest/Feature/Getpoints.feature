Feature: Points


 Scenario Outline: Get Points with product id data
    Given I have token <token>
	And   I have product id <productId>
	And   I GET points 
	Then  The get points respons code should be <statuscode>
	Then  The get points resonpse key should have values
	| key    | value    |
	| UserId | 1        |
	| Points | <points> |
	
	Examples: 
	| productId | token                                | points | statuscode |
	| 0         | 37cb9e58-99db-423c-9da5-42d5627614c5 | 739200 | 202        |
	| 1         | 37cb9e58-99db-423c-9da5-42d5627614c5 | 739100 | 202        |
	| 2         | 37cb9e58-99db-423c-9da5-42d5627614c5 | 739200 | 202        |

Scenario Outline: Get Points with incorrect data
	Given I have token <token>
	And   I have product id <productId>
	And   I GET points 
	Then  The get points respons code should be <statuscode>
	Examples: 
	| CaseID           | token                                | productId | statuscode |
	| InvalideID       | 37cb9e58-99db-423c-9da5-42d5627614c5 | 10        | 400        |
	| ProductIDNull    | 37cb9e58-99db-423c-9da5-42d5627614c5 |           | 500        |
	| ProductIDIllegal | 37cb9e58-99db-423c-9da5-42d5627614c5 |&^%$       | 500        |
	| InvalideToken    | 37cb9e58-99db-423c-9da5-42d5627617c5 | 1         | 401        |
	| TokenNull        |                                      | 1         | 401        |
	| TokenIllegal     | *&^%$#####                           | 1         | 401        |

	
