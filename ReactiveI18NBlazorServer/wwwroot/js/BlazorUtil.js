class BlazorUtil
{
    addCookies(key, value)
    {
        document.cookie = `${key}=${value}`;
    }
}

window.BlazorUtil = new BlazorUtil();