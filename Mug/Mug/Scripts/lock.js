/* lockScreen & unlockScreen */
function lockScreen(message) {
    //鎖定畫面，禁止使用者操作。
    $.blockUI({
        message: function () { if (message) return message; else return "交易進行中...請稍候" },
        css: {
            border: 'none',
            padding: '10px',
            width: '30%',
            top: '45%',
            left: '35%',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            'border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}//lockScreen

function unlockScreen() {
    $.unblockUI();
}//unlockScreen