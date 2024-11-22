Feature: Login


 Scenario Outline: Login test 
	Given I have username <username>
	And   I have password <password>
	And   I POST login 
	And   I got the token
	Then  The respons code should be <statuscode>
	
	Examples: 
	| CaseID            | username | password   | statuscode |
	| HappyPath         | Tester   | Plexure123 | 200        |
	| PwdLowercase      | Tester   | plexure123 | 401        |
	| UsernameLowercase | tester   | Plexure123 | 200        |
	| UsernaleNull      | null     | Plexure123 | 401        |
	| PwdNull           | Tester   | null       | 401        |
	| Null              | Null     | Null       | 401        |
	| UnHappyPath       | Testor   | Plexurr123 | 401        |

