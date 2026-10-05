const productGrid = document.getElementById('productGrid');
const categoryList = document.getElementById("category-list");

function getCategories() {
    fetch("http://localhost:5075/api/Categories")
        .then(response => response.json())
        .then(data => {
            data.forEach(
                category => {
                    const button = document.createElement("button");
                    button.className = "category-btn";
                    button.innerText = category.name;
                    button.addEventListener("click", setCategory);
                    button.setAttribute("data-category", category.id);
                    categoryList.appendChild(button);
                }
            )
        })
}

getCategories();

var productList = [];

function GetProducts() {
    fetch("http://localhost:5000/api/Products")
        .then(response => response.json())
        .then(data => {
            productList = data;
            displayProducts();
        })
}

GetProducts();

function setCategory(event) {
    const selectedCategory = event.target.getAttribute("data-category");
    console.log(selectedCategory);

    fetch(`http://localhost:5000/api/Categories/${selectedCategory}/products`)
        .then(response => response.json())
        .then(data => {
            productList = data;
            displayProducts();
        });
}


function displayProducts() {
    productGrid.innerHTML = '';

    productList.forEach(product => {
        const productCard = document.createElement('div');
        productCard.className = 'product-card';
        productCard.innerHTML = `
            <div class="product-image"></div>
            <h3>${product.name}</h3>
            <div class="product-footer">
                <span class="product-price">${product.price.toFixed(2)} ₼</span>
                <span class="product-category">${product.categoryName}</span>
            </div>
        `;
        productGrid.appendChild(productCard);
    });
}


displayProducts();