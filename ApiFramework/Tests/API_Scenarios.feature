Feature: API-Scenarios


@mytag
Scenario: Simple GET Request
    Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForSimpleGET.json   |
	Then I have a simple GET API response

Scenario: Simple POST Request
	Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForSimplePOST.json   |
	Then I get POST API response

Scenario: GET Request with token generation which expire after 24 hours
	Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForTokenGET.json   |
	Then I get response for API that require token

Scenario: GET request with basic authentication username and password
	Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputBasicAuth.json   |
	Then I get API response for Basic Authorization 




