Feature: Intra-Account Money Transfers & Staff Benefit Enforcement
  As a Nova Premier Bank account holder
  I want to transfer funds between my own Everyday, Investment, and Omni accounts
  So that I can manage my personal finances flexibly while adhering to account overdraft boundaries and staff fee policies.

  Background:
    Given the Nova Premier Bank system is initialized
    And customer "20240040" is registered as a "Regular" customer
    And customer "STAFF01" is registered as a "Staff" customer with a 50% transaction fee discount
    And transfers between different customers are strictly prohibited

  # =========================================================================
  # SCENARIO OUTLINE 1: SUCCESSFUL INTRA-ACCOUNT MONEY TRANSFERS
  # =========================================================================
  @SuccessfulTransfers
  Scenario Outline: Successful fund transfer between owned accounts
    Given customer "<CustomerId>" owns a "<SourceType>" account with balance $<SourceInitial>
    And customer "<CustomerId>" owns a "<DestType>" account with balance $<DestInitial>
    When the customer transfers $<Amount> from "<SourceType>" to "<DestType>"
    Then the transfer should be approved successfully
    And the "<SourceType>" account balance should become $<SourceFinal>
    And the "<DestType>" account balance should become $<DestFinal>
    And the audit status on "<SourceType>" should record "Transfer Out Successful: $<Amount>"
    And the audit status on "<DestType>" should record "Transfer In Successful: $<Amount>"

    Examples:
      | CustomerId | SourceType | SourceInitial | DestType   | DestInitial | Amount | SourceFinal | DestFinal | Note                             |
      | 20240040   | Everyday   | 500.00        | Investment | 2000.00     | 200.00 | 300.00      | 2200.00   | Regular: Everyday to Investment  |
      | 20240040   | Investment | 2000.00       | Omni       | 1500.00     | 500.00 | 1500.00     | 2000.00   | Regular: Investment to Omni      |
      | 20240040   | Omni       | 1500.00       | Everyday   | 500.00      | 400.00 | 1100.00     | 900.00    | Regular: Omni to Everyday        |
      | STAFF01    | Everyday   | 1000.00       | Omni       | 500.00      | 350.00 | 650.00      | 850.00    | Staff: Everyday to Omni          |
      | STAFF01    | Investment | 3000.00       | Everyday   | 1000.00     | 750.00 | 2250.00     | 1750.00   | Staff: Investment to Everyday    |
      | 20240040   | Omni       | 1500.00       | Everyday   | 500.00      | 1800.00| -300.00     | 2300.00   | Omni: Overdraft used (limit $500)|

  # =========================================================================
  # SCENARIO OUTLINE 2: FAILURE STATES - OVERDRAFT AND BALANCE EXCEEDED
  # =========================================================================
  @TransferFailures
  Scenario Outline: Failed transfer exceeding account limits or balance
    Given customer "<CustomerId>" has account role "<CustomerRole>"
    And owns a "<SourceType>" account with balance $<SourceInitial> and overdraft limit $<OverdraftLimit>
    And owns a "<DestType>" account with balance $<DestInitial>
    When the customer attempts to transfer $<Amount> from "<SourceType>" to "<DestType>"
    Then the transfer should be rejected with an InsufficientFundsException
    And a failed transaction fee of $<ExpectedFee> should be deducted from "<SourceType>"
    And the final balance of "<SourceType>" should be $<SourceFinal>
    And the destination "<DestType>" balance should remain unchanged at $<DestInitial>
    And the audit message should state "<ExpectedStatus>"

    Examples:
      | CustomerId | CustomerRole | SourceType | SourceInitial | OverdraftLimit | DestType   | DestInitial | Amount  | ExpectedFee | SourceFinal | ExpectedStatus                            |
      | 20240040   | Regular      | Everyday   | 500.00        | 0.00           | Investment | 2000.00     | 600.00  | 0.00        | 500.00      | Overdraft is not permitted on Everyday    |
      | 20240040   | Regular      | Investment | 2000.00       | 0.00           | Everyday   | 500.00      | 2500.00 | 10.00       | 1990.00     | Failed transaction fee of $10.00 charged  |
      | 20240040   | Regular      | Omni       | 1500.00       | 500.00         | Everyday   | 500.00      | 2050.00 | 10.00       | 1490.00     | Overdraft limit exceeded. Fee: $10.00     |
      | STAFF01    | Staff        | Everyday   | 500.00        | 0.00           | Investment | 2000.00     | 750.00  | 0.00        | 500.00      | Overdraft is not permitted on Everyday    |
      | STAFF01    | Staff        | Investment | 2000.00       | 0.00           | Everyday   | 500.00      | 3000.00 | 5.00        | 1995.00     | Failed transaction fee of $5.00 charged   |
      | STAFF01    | Staff        | Omni       | 1500.00       | 500.00         | Everyday   | 500.00      | 2200.00 | 5.00        | 1495.00     | Overdraft limit exceeded. Fee: $5.00      |

  # =========================================================================
  # SCENARIO OUTLINE 3: BANK STAFF BENEFIT (50% DISCOUNT) ENFORCEMENT
  # =========================================================================
  @StaffFeeLogic
  Scenario Outline: Bank Staff Benefit guarantees 50% discount on failed fees
    Given a customer with ID "<CustomerId>" and staff status "<IsStaff>"
    When a failed transfer fee of baseline $<StandardFee> is incurred on account "<AccountType>"
    Then the calculated fee charged to the customer balance should be $<ExpectedChargedFee>
    And the staff discount applied should equal $<ExpectedDiscount>

    Examples:
      | CustomerId | IsStaff | AccountType | StandardFee | ExpectedChargedFee | ExpectedDiscount | Note                                |
      | 20240040   | False   | Investment  | 10.00       | 10.00              | 0.00             | Regular customer pays full standard |
      | 20240040   | False   | Omni        | 10.00       | 10.00              | 0.00             | Regular customer pays full standard |
      | STAFF01    | True    | Investment  | 10.00       | 5.00               | 5.00             | Staff receives 50% benefit discount |
      | STAFF01    | True    | Omni        | 10.00       | 5.00               | 5.00             | Staff receives 50% benefit discount |

  # =========================================================================
  # SCENARIO OUTLINE 4: INPUT INTEGRITY & SECURITY RESTRICTIONS
  # =========================================================================
  @InputValidation
  Scenario Outline: Transfer input integrity and boundary validation
    Given customer "20240040" owns accounts "ED-20240040" and "INV-20240040"
    When an invalid transfer is requested from "<Source>" to "<Destination>" with amount $<Amount>
    Then the system should reject the request with a BankingException stating "<ErrorMessage>"

    Examples:
      | Source      | Destination  | Amount | ErrorMessage                                           |
      | ED-20240040 | ED-20240040  | 100.00 | Source and destination accounts cannot be the same     |
      | ED-20240040 | INV-20240040 | -50.00 | Amount must be positive                                |
      | ED-20240040 | INV-20240040 | 0.00   | Amount must be positive                                |
      | ED-20240040 | ED-STAFF01   | 100.00 | Transfers are strictly intra-account                  |
