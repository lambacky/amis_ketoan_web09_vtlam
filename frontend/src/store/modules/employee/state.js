const state = {
    dialogTitle: "Thêm khách hàng",
    isShowDialog: false,
    formMode: 0,
    filter:{
        pageSize:10,
        pageNumber:1,
        employeeFilter:null
    },
    employees: [],
    employee:{},
    singleEmployee:{},
    totalEmployee: 0,
    totalPage: 0,
    checkedEmployeeIds: []
}
export default state