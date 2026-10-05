const loginForm = document.getElementById('loginForm');
const errorMessage = document.getElementById('errorMessage');
const loginBtn = document.querySelector('.login-btn');
const btnText = document.querySelector('.btn-text');
const btnLoader = document.querySelector('.btn-loader');

loginForm.addEventListener('submit', handleLogin);

function handleLogin(event) {
    event.preventDefault();


    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    fetch('http://localhost:5000/api/Auth/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({email, password})
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('İstifadəçi adı və ya şifrə yalnışdır.');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
            var token = data.token;
            localStorage.setItem("authToken", token);
            window.location.href = 'index.html';
        }).catch(error => {
        showError(error.message);
    });


    setLoading(true);
    hideError();
}


function setLoading(isLoading) {
    loginBtn.disabled = isLoading;

    if (isLoading) {
        btnText.style.display = 'none';
        btnLoader.style.display = 'inline-block';
    } else {
        btnText.style.display = 'inline-block';
        btnLoader.style.display = 'none';
    }
}

function showError(message) {
    errorMessage.textContent = message;
    errorMessage.style.display = 'block';
}

function hideError() {
    errorMessage.style.display = 'none';
}