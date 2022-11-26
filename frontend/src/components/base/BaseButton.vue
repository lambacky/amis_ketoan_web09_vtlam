<template>
    <button class="btn" :class="{'btn-secondary' : isSecondary}" :disabled="isDisabled" :tabindex="tabIndex">{{btnText}}</button>
</template>

<script>
export default {
    name:"BaseButton",
    props:{
        isSecondary:{
            type: Boolean,
            default: false
        },
        btnText: {
            type: String,
            default:"Button"
        },
        isDisabled: {
            type: Boolean,
            default:false
        },
        tabIndex:{
            type: String,
            default:"0"
        }
    }
}
</script>