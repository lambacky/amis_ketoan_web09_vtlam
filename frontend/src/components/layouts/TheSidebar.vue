<template>
    <div class="sidebar">
        <div class="sidebar-logo">
            <div class="option-icon"></div>
            <img class="logo-icon" src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="Logo_Module_TiengViet_White" />
            <div class="icon sidebar-icon icon-three-stripes"></div>
        </div>
        <div class="sidebar-menu">
            <a href="" class="menu-item">
                <div class="icon item-icon icon-dashboard"></div>
                <div class="item-title">{{SideBarItem.dashboard}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-cash"></div>
                <div class="item-title">{{SideBarItem.cash}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-bank"></div>
                <div class="item-title">{{SideBarItem.bank}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-buy"></div>
                <div class="item-title">{{SideBarItem.buy}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-sale"></div>
                <div class="item-title">{{SideBarItem.sale}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-invoice"></div>
                <div class="item-title">{{SideBarItem.invoice}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-stock"></div>
                <div class="item-title">{{SideBarItem.stock}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-tools"></div>
                <div class="item-title">{{SideBarItem.tools}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-fixed-assets"></div>
                <div class="item-title">{{SideBarItem.fixedAssets}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-tax"></div>
                <div class="item-title">{{SideBarItem.tax}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-price"></div>
                <div class="item-title">{{SideBarItem.price}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-general"></div>
                <div class="item-title">{{SideBarItem.general}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-budget"></div>
                <div class="item-title">{{SideBarItem.budget}}</div>
            </a>
            <a href="" class="menu-item">
                <div class="icon item-icon icon-report"></div>
                <div class="item-title">{{SideBarItem.report}}</div>
            </a>
            <!-- <a href="" class="menu-item">
                <div class="icon item-icon icon-finance"></div>
                <div class="item-title">{{SideBarItem.finance}}</div>
            </a> -->
        </div>
    </div>
</template>

<script>
import resourceVN from "../../resources/resourceVN"
export default {
    name:"TheSidebar",
    components:{},
    props:[],
    methods:{},
    data() {
        return {
            SideBarItem:resourceVN.SideBarItem
        }
    },
}
</script>