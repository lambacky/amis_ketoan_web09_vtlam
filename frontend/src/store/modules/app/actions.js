const actions = {

    /**
     * Ẩn/hiện loading
     * @param {*} context 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    toggleLoading(context){
        context.commit("toggleLoading");
    },

    /**
     * Ẩn/hiện cảnh báo
     * @param {*} context 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    toggleAlert(context) {
        context.commit("toggleAlert");
    },

    /**
     * Ẩn/hiện toast message
     * @param {*} context 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    toggleToastMessage(context) {
        context.commit("toggleToastMessage");
    },

    /**
     * Xử lí nội dung của cảnh báo
     * @param {*} context 
     * @param {*} alert 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    setAlert(context,alert){
        context.commit("setAlert",alert);
        context.dispatch("toggleAlert");
    },

    /**
     * Xử lí nội dung của toast message
     * @param {*} context 
     * @param {*} alert 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    setToastMessage(context,toastMessage){
        context.commit("setToastMessage",toastMessage);
        context.dispatch("toggleToastMessage");
    }
}

export default actions