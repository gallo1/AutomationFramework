Feature: ChallengeFeature


@mytag
Scenario: ChallengeRun
Given I am on the Challenge Home Page and click the 'Start the Challenge Button' 
Then I should be on the 'Challenge 1' Page
Given I am now on the 'Challenge 1' page I should complete the challenge
Then I should be on the 'Challenge 2' Page
Given I am now on the 'Challenge 2' page I should complete the challenge
Then I should be on the 'Challenge 3' Page
Given I am now on the 'Challenge 3' page I should complete the challenge
Then I should be on the 'Challenge 4' Page
Given I am now on the 'Challenge 4' page I should complete the challenge
Then I should be on the 'Challenge 5' Page
Given I am now on the 'Challenge 5' page I should complete the challenge
Then I should see the 'Complete!' page title

Scenario: ChallengeFailure
Given I am on the Challenge Home Page and click the 'Start the Challenge Button'
Then I should be on the 'Challenge One' Page

Scenario: ChallengeOne
Given I am on the Challenge Home Page and click the 'Start the Challenge Button'
Then I should be on the 'Challenge 1' Page

Scenario: ChallengeTwo 
Given I am on the Challenge Home Page and click the 'Start the Challenge Button'
Then I should be on the 'Challenge 1' Page
Given I am now on the 'Challenge 1' page I should complete the challenge
Then I should be on the 'Challenge 2' Page

Scenario:  ChallengeThree
Given I am on the Challenge Home Page and click the 'Start the Challenge Button'
Then I should be on the 'Challenge 1' Page
Given I am now on the 'Challenge 1' page I should complete the challenge
Then I should be on the 'Challenge 2' Page
Given I am now on the 'Challenge 2' page I should complete the challenge
Then I should be on the 'Challenge 3' Page

Scenario: ChallengeFour
Given I am on the Challenge Home Page and click the 'Start the Challenge Button'
Then I should be on the 'Challenge 1' Page
Given I am now on the 'Challenge 1' page I should complete the challenge
Then I should be on the 'Challenge 2' Page
Given I am now on the 'Challenge 2' page I should complete the challenge
Then I should be on the 'Challenge 3' Page
Given I am now on the 'Challenge 3' page I should complete the challenge
Then I should be on the 'Challenge 4' Page

Scenario: ChallengeFive
Given I am on the Challenge Home Page and click the 'Start the Challenge Button'
Then I should be on the 'Challenge 1' Page
Given I am now on the 'Challenge 1' page I should complete the challenge
Then I should be on the 'Challenge 2' Page
Given I am now on the 'Challenge 2' page I should complete the challenge
Then I should be on the 'Challenge 3' Page
Given I am now on the 'Challenge 3' page I should complete the challenge
Then I should be on the 'Challenge 4' Page
Given I am now on the 'Challenge 4' page I should complete the challenge
Then I should be on the 'Challenge 5' Page