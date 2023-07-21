const newCurrenciesListSessionKey = "newCurrencyOptions";
var currenciesList = document.getElementsByClassName("currencies")[0];

let customCurrencies = sessionStorage.getItem(newCurrenciesListSessionKey);
if (customCurrencies) {
    let list = JSON.parse(customCurrencies);
    for (let i = 0; i < list.length; i++) {
        let newOption = document.createElement("option");
        newOption.value = list[i];
        newOption.innerText = list[i];
        currenciesList.append(newOption);
    }
}

var toolTip = document.getElementsByClassName("accordion");
for (let i = 0; i < toolTip.length; i++) {
    toolTip[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.display === "block") {
            panel.style.display = "none";
        } else {
            panel.style.display = "block";
        }
    });
}

var moon = document.getElementsByClassName("moon")[0];
var sun = document.getElementsByClassName("sun")[0];

var styleLinks = Array.from(document.getElementsByTagName('link'));
var linkDay = styleLinks.find(x => x.getAttribute("href").includes("site.css"));
var linkNight = styleLinks.find(x => x.getAttribute("href").includes("night.css"));
if (moon) {
    moon.addEventListener("click", () => {
        moon.style.display = 'none';
        sun.style.display = 'block';
        localStorage.setItem("viewDayOption", JSON.stringify("night"));
        linkNight.disabled = false;
    });
}
if (sun) {
    sun.addEventListener("click", () => {
        moon.style.display = 'block';
        sun.style.display = 'none';
        localStorage.setItem("viewDayOption", JSON.stringify("day"));
        linkNight.disabled = true;
    });
}

let viewOption = localStorage.getItem("viewDayOption");
if (viewOption) {
    let option = JSON.parse(viewOption);
    if (option == "day") {
        moon.style.display = 'block';
        sun.style.display = 'none';
        linkNight.disabled = true;
    }
    if (option == "night") {
        moon.style.display = 'none';
        sun.style.display = 'block';
        linkNight.disabled = false;
    }
}

var addNewCurrencyBtw = document.getElementsByClassName("add-custom-currency")[0];
var removeCustomCurrencyBtw = document.getElementsByClassName("remove-all-custom-currencies")[0];
var opacityEffect = document.getElementsByClassName("opacity-effect")[0];
var addNewCurrencyWind = document.getElementsByClassName("add-new-currency-wind")[0];
var convertBtw = document.getElementsByClassName("convert-sbmt")[0];

removeCustomCurrencyBtw.addEventListener("click", () => {
    let customCurrencies = sessionStorage.getItem(newCurrenciesListSessionKey);
    if (customCurrencies) {
        sessionStorage.removeItem(newCurrenciesListSessionKey);
        window.location.reload();
        alert("Deleted");
    }
});

addNewCurrencyBtw.addEventListener("click", () => {
    opacityEffect.style.opacity = "0.1";
    addNewCurrencyWind.style.display = "flex";
    addNewCurrencyBtw.disabled = true;
    convertBtw.disabled = true;
    currenciesList.disabled = true;
    let saveCurrencyBtw = document.getElementsByClassName("save-new-currency-btw")[0];
    let closeCurrencyBtw = document.getElementsByClassName("close-currency-wind")[0];

    closeCurrencyBtw.addEventListener("click", () => {
        addNewCurrencyWind.style.display = "none";
        addNewCurrencyBtw.disabled = false;
        convertBtw.disabled = false;
        currenciesList.disabled = false;
        opacityEffect.style.opacity = "1";
    });

    saveCurrencyBtw.addEventListener("click", () => {
        let nameToAdd = document.getElementsByClassName("to-add-currency-name")[0];
        if ((nameToAdd.value === undefined || nameToAdd.value === null || nameToAdd.value === '') ||
            (nameToAdd.value && (nameToAdd.value.length < 3 || nameToAdd.value.trim() === '')) ) {
            alert("Name was invalid, minimum name's length is 3 symbols");
            return;
        }
        addNewCurrencyBtw.disabled = false;
        convertBtw.disabled = false;
        currenciesList.disabled = false;
        opacityEffect.style.opacity = "1";
        addNewCurrencyWind.style.display = "none";
        let newOption = document.createElement("option");
        newOption.value = nameToAdd.value;
        newOption.innerText = nameToAdd.value;
        let options = sessionStorage.getItem(newCurrenciesListSessionKey);
        if (options) {
            let list = JSON.parse(options);
            list.push(nameToAdd.value);
            sessionStorage.setItem(newCurrenciesListSessionKey, JSON.stringify(list));
        } else {
            let list = [];
            list.push(nameToAdd.value);
            sessionStorage.setItem(newCurrenciesListSessionKey, JSON.stringify(list));
        }
        currenciesList.append(newOption);
    });
});

