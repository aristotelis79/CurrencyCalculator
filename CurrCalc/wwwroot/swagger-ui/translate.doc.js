
const Language = {

    select_list_id_Selector: "languages",
    topBar_Selector: ".topbar-wrapper",
    language_Key_Selector: "data-lang-key",
    close_block_Selector: ".opblock-tag",
    block_summary_Selector: ".opblock-summary-description",
    data_is_open_Selector: 'data-is-open',
    localize_key_start : "Localize_Key_",
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
        document.querySelector(Language.topBar_Selector).append(listEl);

        //get languages
        getData(Language.languagesUrl).then((data) => {
            toSelectList(`#${Language.select_list_id_Selector}`, data.data);
        });

        Language.setKeys();

        Language.getEnglish();

        Language.collapseEvent();
    },

    setKeys: function() {
        Language.descriptionsEls = document.querySelectorAll(Language.block_summary_Selector);
        Language.descriptionsEls.forEach(e => {
            if(e.innerText.startsWith(Language.localize_key_start))
                e.setAttribute(Language.language_Key_Selector, e.innerText);
        });
    },

    getEnglish: function() {
        getData(Language.localizeTextsUrl+ '/en').then((data) => {
            Language.sources = data.data;
            Language.replaceLanguageSources();
        });
    },

    collapseEvent: function() {
        const blocks = document.querySelectorAll(Language.close_block_Selector);
        blocks.forEach((b) => {
            b.onclick = function() {
                if (b.getAttribute(Language.data_is_open_Selector) === 'false') {
                    setTimeout(function() {
                        Language.setKeys();
                        Language.replaceLanguageSources();
                    }, 1000);
                }
            }
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
            const key = e.getAttribute(Language.language_Key_Selector);
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
