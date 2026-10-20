# Təhlükəsizlik siyasəti

Zəiflik tapsanız, lütfən public issue açmayın. Bu repo-nun "Security" bölməsindəki
"Report a vulnerability" düyməsi ilə məxfi şəkildə bildirin.

## Security maintenance (2026-10-20)

Unused legacy Chart.js, DataTables and Bootstrap 4 JavaScript assets under
`Homework/PustokApp/PustokApp/wwwroot/admin/vendor` were removed. No application
views or scripts referenced these JavaScript assets; the active `wwwroot/lib` assets remain.
Bootstrap Sass sources are retained because the admin theme imports them.
This removes the vulnerable code itself without excluding it from a scanner.
Direct and transitive NuGet packages are audited on pushes, pull requests and weekly.

## CodeQL triage and deployment boundary

`cs/web/xss` in WebApplicationConsume/CategoryController flags `StringContent`
used in an outbound `HttpClient.PostAsync` multipart request to the local API.
Both fields explicitly use `text/plain`; neither is an HTML response to the browser.
`MultipartTests` verifies the destination, content type and unchanged field values.
Razor remains responsible for output encoding when these values are displayed.

`cs/web/missing-token-validation` in FirstApiApp and MenuApp flags JSON or multipart
API endpoints. Protected endpoints authenticate the explicit Authorization bearer
header. Login returns a token in the response body and does not create a session
cookie. The separate cookie-backed WebApplicationConsume UI validates CSRF tokens.
MiniAppAPI and OnionArchApp have no cookie/session authentication at all, so their
CSRF findings do not identify an ambient-credential attack.

**MiniAppAPI and OnionArchApp are unauthenticated coursework demos. Keep them local;
do not expose them to the internet or use real data without implementing access
control.** A clean scan does not make a teaching application production-ready.
Do not add cookie authentication to these APIs without CSRF protection and tests.
