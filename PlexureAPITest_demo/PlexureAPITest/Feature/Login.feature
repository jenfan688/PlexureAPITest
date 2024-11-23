Feature: Login


 Scenario Outline: Happy path Login test 
	Given I have username <username>
	And   I have password <password>
	And   I POST login
	Then  The login respons code should be <statuscode>
	Then  The login respons body should be <responseBody> except "AccessToken"
	
	
	Examples: 
	| CaseID            | username | password   | statuscode |  responseBody       |
	| HappyPath         | Tester   | Plexure123 | 200        | LoginResponse1.json |
	| UsernameLowercase | tester   | Plexure123 | 200        | LoginResponse2.json |


 Scenario Outline: Unhappy path of Login test 
	Given I have username <username>
	And   I have password <password>
	And   I POST login
	Then  The login respons code should be <statuscode>

	Examples: 
	| CaseID            | username | password   | statuscode | 
	| PwdLowercase      | Tester   | plexure123 | 401        | 
	| UsernaleNull      | null     | Plexure123 | 401        | 
	| PwdNull           | Tester   | null       | 401        | 
	| Null              |          |            | 400        | 
	| UnHappyPath       | Testor   | Plexurr123 | 401        | 
