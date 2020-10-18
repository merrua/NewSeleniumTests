Feature: WD Landing Page Filters
    Web Disclosure (WD) is a public site where clients can make the votes public.

	In order to find what I'm looking for
	As a user
	I want to be told show the correct filter result


Scenario: Can use a meeting filter by Country - Belgium 
  Given user is on the landing page for WD site
  And the Country filter is available
  When user selects 'Belgium' from the Country filter list on left panel
  And clicks on Update button for the country filter list
  Then the grid displays all meetings that are associated with the country 'Belgium'
  And no meetings associated with any other country appear on the list

Scenario: Can go to vote card page for a particular company - Activision Blizzard Inc
  Given user is on the landing page for WD site
  When user clicks the Company Name 'Activision Blizzard Inc' hyperlink
  Then the user lands onto the 'Activision Blizzard Inc' vote card page.
  And 'Activision Blizzard Inc' should appear in the top banner