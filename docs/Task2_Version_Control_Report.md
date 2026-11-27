# Task 2: Advanced Version Control & Feature Branching Report

**Project:** Nova Premier Bank — Sprint 3 (Data Persistence & Final Delivery)  
**Student Name:** Muskan Panwar  
**Student ID:** 20240040  
**Course:** IT7742 - Assessment 3  
**Institution:** Whitecliffe College of Technology & Innovation  

---

## 1. Feature Branching Workflow Summary

In accordance with Task 2 requirements, all Sprint 3 development was isolated from the `main` / `master` release branch on a dedicated feature branch named **`feature/sprint3-integration`**.

### Key Practices Implemented:
1. **`.gitignore` Configuration**:
   - Created a comprehensive `.gitignore` in the repository root ignoring `.vs/`, `bin/`, `obj/`, `TestResults/`, `*.user`, `*.suo`, and `.deps.json` / `.runtimeconfig.json` artifacts.
   - Untracked previously cached IDE and build artifacts using `git rm -r --cached` so the repository remains clean and lightweight.
2. **Staged, Descriptive Commit History**:
   - `5979b1c` — `Chore: Add .gitignore and untrack build artifacts`
   - `8fd7c3e` — `Feat(Architecture): Add polymorphic JSON persistence, dynamic account creation, and intra-account transfers`
   - `5e28111` — `Feat(UI): Implement Form Inheritance, Intra-Account Transfer Dialog, Dynamic Account Creation, and JSON Persistence Triggers`
   - `afa9450` — `Test(QA): Add 19 automated tests for transfers, overdraft limits, staff discounts, and JSON persistence`
   - `8ffea83` — `Docs(Handover): Add Gherkin features, UML report, UI wireframes, persistence sequence diagrams, test table, XML report, and 3-page User Guide`
3. **Release Merge (`--no-ff`)**:
   - Once all features and tests were verified passing (65/65 tests), `feature/sprint3-integration` was merged into `main` (and synchronized to `master`) via a non-fast-forward merge commit (`3c6a620`):
     `Release(Sprint 3): Merge feature/sprint3-integration into main for Assessment 3 Final Delivery`

---

## 2. Git Graph Evidence

```text
*   3c6a620 Release(Sprint 3): Merge feature/sprint3-integration into main for Assessment 3 Final Delivery
|\
| * 8ffea83 Docs(Handover): Add Gherkin features, UML report, UI wireframes, persistence sequence diagrams, test table, XML report, and 3-page User Guide
| * afa9450 Test(QA): Add 19 automated tests for transfers, overdraft limits, staff discounts, and JSON persistence
| * 5e28111 Feat(UI): Implement Form Inheritance, Intra-Account Transfer Dialog, Dynamic Account Creation, and JSON Persistence Triggers
| * 8fd7c3e Feat(Architecture): Add polymorphic JSON persistence, dynamic account creation, and intra-account transfers
| * 5979b1c Chore: Add .gitignore and untrack build artifacts
| * b38c717 Sprint 2: Add new code
|/
* 023756c Edited some classes of accounts and link to repository
* c6b01fb CLI task added all the classes and files created
* ad471a4 UML and task 1 completed as per assignment scope
```

---

## 3. Submission Checklist Verification

- [x] **Feature branch created:** `feature/sprint3-integration`
- [x] **`.gitignore` active:** `bin/`, `obj/`, and `.vs/` excluded from version control
- [x] **Meaningful commit messages:** Conventional Commits (`Chore:`, `Feat:`, `Test:`, `Docs:`, `Release:`)
- [x] **Merged into `main` / `master`:** Release merge commit `3c6a620`
- [x] **Clean working tree:** Verified all source files compile and all 65 tests pass
