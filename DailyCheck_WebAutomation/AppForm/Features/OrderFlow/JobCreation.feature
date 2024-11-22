Feature: JobCreation

Validation of Job Creation feature

Background: 
	Given Launch the solo browser
	When User login with valid solo credentials
	And Click on Advanced job search link

@Smoke
Scenario: TC001_SOLO_Login_Advanced Job Search
Then Verify the display of Advanced Job Search screen
	
Scenario: TC002_SOLO_Login_Advanced Job Search_Screen element_Verify
Then Verify the screen elements of Advanced Job Search screen
 
Scenario: TC003_SOLO_Login_Advanced Job Search_Add Job_button_Validation
Given Click on the My Job button
Then Click on the Add Job button

Scenario: TC004_SOLO_Login_Add Job forms_Verify
Given Click on the My Job button
Then Click on the Add Job button
Then Verify the mandatory UI elements in Add job Form screen

Scenario: TC005_SOLO_Login_Add Job forms_Validate valid data
Given Click on the My Job button
Then Click on the Add Job button
When Select the Contract from dropdown
#When Select the Exchange Area from dropdown
#And Enter the value in Estimate No field
#And Select the Job Category from dropdown
#And Enter the value in Required by Date/Time field
#And Enter the value in Originator field
#And Enter the value in OCU field
#And Enter the value in Receipt Date/Time field
#Then Select the Depot from dropdown
#Then Enter the value in COW field
#Then Enter the value in Location field

#Scenario: TC006_SOLO_Login_Add Job forms_Valid data_Save button_validation
#Given Click on the My Job button
#And Click on the Add Job button
#And Enter the valid data in Add Job screen
#And Click on Save button
#Then Verify the saved details in Add Job screen

#Scenario: TC007_SOLO_Login_Add Job Screen_Order status_Verify
#Given Click on the My Job button
#And Click on the Add Job button
#And Enter the valid data in Add Job screen
#And Click on Save button
#Then Verify the Order status in job info screen

#Scenario: TC008_SOLO_Login_Add Job_Estimate_Validation
#Given Click on the My Job button
#And Click on the Add Job button
#And Enter the valid data in Add Job screen
#And Click on Save button
#Then Click on the Estimate in Job details screen

#Scenario: TC009_SOLO_Login_Add Job_Estimate_Add_button_Validation
#Given Click on the My Job button
#And Click on the Add Job button
#And Enter the valid data in Add Job screen
#And Click on Save button
#And Click on the Estimate in Job details screen
#Then Click on the Add button in Estimate screen

#Scenario: TC010_SOLO_Login_Add Job_Estimate form_ valid data Validation
#Given Click on the My Job button
#And Click on the Add Job button
#And Enter the valid data in Add Job screen
#And Click on Save button
#And Click on the Estimate in Job details screen
#And Click on the Add button in Estimate screen
#And Click on "Desciption" dropdown and Select any option under the dropdown
#And Enter the value in "Est Qty" text box field
#Then Verify the filled values in Estimate screen
#
#Scenario: TC011_SOLO_Login_Add Job_Estimate_Add_Estimate form_valid data_Save button_Validation
#Given Click on the My Job button
#And Click on the Add Job button
#And Enter the valid data in Add Job screen
#And Click on Save button
#And Click on the Estimate in Job details screen
#And Click on the Add button in Estimate screen
#And Click on "Desciption" dropdown and Select any option under the dropdown
#And Enter the value in "Est Qty" text box field
#Then Click on "Save" button
#
#
#When User login with valid Solo credentials
#When Double click on the order number in Quick link home page
#And Click on the "Estimate" in Job info screen
#And Click on the add button
#And Click on "Desciption" dropdown and Select any option under the dropdown
#And Enter the value in "Est Qty" text box field
#Then Click on "Save" button
#
#Scenario: TC012_SOLO_Login_Add job forms_Estimate_"Accept job"_Validation
#Given Launch the Solo browser
#When User login with valid Solo credentials
#When Double click on the order number in Quick link home page
#When Click on the "Accept job" button in job info screen
#And Click on "Continue" button in A537 location validation popup screen
#And Click on the "NO" button in Confirmation popup screen
#Then Click on the "continue with job" in Jobpack documents screen
#
#Scenario: TC014_SOLO_Login_Add Job_Estimate_Accept job_"Release to Depot"_Validate
#Given Launch the Solo browser
#When User login with valid Solo credentials
#When Double click on the order number in Quick link home page
#Given Click on the "Release to depot" in job info screen
#When Click on the "Save" button in mandatory work flow
#And Click the radio button of "NOT ZOI" field in Release job popup
#And Click the "Release" button in Release job popup
#Then ClickOnTheReleaseJobConfirmationYesButton
#
#Scenario: TC015_SOLO_Login_Add job forms_Estimate_Release to depot_"Depot Accepted"_Order status_Verify
#Given Launch the Solo browser
#When User login with valid Solo credentials
#When Double click on the order number in Quick link home page
#When AcceptTheJob
#And Click on "Poling Weighting" dropdown and Select any option under the dropdown
#And Click on the "Continue" button in Poling weighting popup screen
#Then click on the Ok button in Status Updated to Depot Accepted popup
#
#Scenario: TC016_SOLO_Login_Add job forms_Estimate_Accept job_ Released to Depot_Depot accepted_"Depot Opened" job_Validate
#Given Launch the Solo browser
#When User login with valid Solo credentials
#When Double click on the order number in Quick link home page
#When AcceptTheJob
#
#Scenario: TC0016_SOLO_Login_Add job forms_Estimate_Acceept job_ Released to Depot_Depot Accepted_Depot Opened_Click on the "Quick Programming" in job info screen
#Given Select the "Date"in date field in Quick programming popup screen
#When Enter the "Duration" in Quick programming Popup screen
#When Click on "Type" dropdown and Select any option under the dropdown
#And Click on "Working Days" dropdown and Select any option under the dropdown
#And Click on "Gang" dropdown and Select any option under the dropdown
#Then Click on the Confirm Checkbox 
#Then Click on the "Save button"
#Then Click on the Popup "OK" buttons





#Scenario: TC003_SOLO_Login_Validation Job Queue_search criteria_Validate
#	Given Launch the solo browser
#	When User login with valid solo credentials
#	When Click ‘Advanced Job Search’ link
#	And Click on eBus Checkbox
#	And Select the Workstream
#	Then Verify the filled elements


	