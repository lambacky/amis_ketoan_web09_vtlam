const getters = {
    dialogTitle: state => state.dialogTitle,
    isShowDialog: state => state.isShowDialog,
    formMode: state => state.formMode,
    filter:state => state.filter,
    employees: state => state.employees,
    employee:state => state.employee,
    singleEmployee:state => state.singleEmployee,
    totalEmployee: state => state.totalEmployee,
    totalPage: state => state.totalPage,
    checkedEmployeeIds: state => state.checkedEmployeeIds
}
export default getters