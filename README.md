### Profile Structure

Profiles are organized by team/environment/tenant:
```
API.TestBase/Config/Profiles/
├── Source/                    # Source files (gitignored)
│   └── 3E-Proforma/
│       ├── dev/
│       │   └── ptpd68r3nke7q5pnutzaaw.json
│       └── test/
│           └── q7v1n2oexe2yohe1ttb9yq.json
└── Encrypted/                 # Encrypted files (committed)
    └── 3E-Proforma/
        ├── dev/
        │   └── ptpd68r3nke7q5pnutzaaw.json
        └── test/
            └── q7v1n2oexe2yohe1ttb9yq.json
```