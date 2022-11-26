const mutations = {
    toggleLoading(state){
        state.isShowLoading=!state.isShowLoading;
    },
    toggleAlert(state){
        state.isShowAlert=!state.isShowAlert;
    },
    toggleToastMessage(state){
        state.isShowToastMessage=!state.isShowToastMessage;
    },
    setAlert(state,payload){
        state.alert = payload;
    },
    setToastMessage(state,payload){
        state.toastMessage = payload;
    }
}
export default mutations