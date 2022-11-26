import AlertAction from "@/enums/alertAction";
const state = {
    isShowLoading:false,
    isShowAlert: false,
    isShowToastMessage:false,
    alert:{
        type:"",
        message:"",
        action:AlertAction.DEFAULT,
    },
    toastMessage:{
        type:"success",
        message:"",
    }
}
export default state