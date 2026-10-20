# Təhlükəsizlik siyasəti

Zəiflik tapsanız, lütfən public issue açmayın. Bu repo-nun "Security" bölməsindəki
"Report a vulnerability" düyməsi ilə məxfi şəkildə bildirin.

## Security maintenance (2026-10-20)

Unused legacy Chart.js, DataTables and Bootstrap 4 assets under
`Homework/PustokApp/PustokApp/wwwroot/admin/vendor` were removed. No application
views or scripts referenced these folders; the active `wwwroot/lib` assets remain.
This removes the vulnerable code itself without excluding it from a scanner.
Direct and transitive NuGet packages are audited on pushes, pull requests and weekly.
