
const Language = {

    select_list_id : "languages",
    topBarSelector : ".topbar-wrapper",
    languageKeySelector: "data-lang-key",
    select_list_html: `<label for="languages" style="color:white">&nbsp&nbsp</label>
                        <select onchange="Language.changeLanguage(this.value)" id="languages">
                        </select>`,

    languagesUrl : "/languages",
    localizeTextsUrl : "/localizeTexts",

    descriptionsEls : { },

    sources: {},

    init: function () {

        //add select list html item
        const listEl = document.createElement("div");
        listEl.innerHTML = Language.select_list_html;
        document.querySelector(Language.topBarSelector).append(listEl);

        //get languages
        getData(Language.languagesUrl).then((data) => {
            toSelectList(`#${Language.select_list_id}`, data.data);
        });

        //set keys
        Language.descriptionsEls = document.querySelectorAll(".opblock-summary-description");
        Language.descriptionsEls.forEach(e => {
            if(e.innerText)
                e.setAttribute(Language.languageKeySelector, e.innerText);
        });

        //get english
        getData(Language.localizeTextsUrl+ '/en').then((data) => {
            Language.sources = data.data;
            Language.replaceLanguageSources();
        });
    },

    changeLanguage: function (val) {
        getData(`${Language.localizeTextsUrl}/${val}`).then((data) => {
            Language.sources = data.data;
            Language.replaceLanguageSources();
        });
    },

    replaceLanguageSources: function () {
        Language.descriptionsEls.forEach(e => {
            const key = e.getAttribute(Language.languageKeySelector);
            if (key)
                e.innerText = Language.sources[key];
        });

    }
}


async function getData(url = '') {
    const response = await fetch(url, {
        method: 'GET',
        mode: 'cors',
        cache: 'default',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer'
    });
    return await response.json();
}


function toSelectList(selector, data) {
    const list = document.querySelector(selector);

    data.forEach(d => {
        const opt = document.createElement("option");
        opt.setAttribute("value", d.value);
        opt.innerHTML = d.text;
        list.append(opt);
    });
}


window.onload = setTimeout(function () {
    Language.init();
}, 2000);
