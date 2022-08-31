<template>
	<div class="home">
		<div class="ssp-button-view__inputs-cw">
			<input type="checkbox" v-model="active" />Active
			<input type="checkbox" v-model="borderless" />Borderless
			<input type="checkbox" v-model="plain" />Plain
			<input type="checkbox" v-model="disabled" />Disabled
			<input type="checkbox" :checked="store.menu.Split" @change="OnSplitMenuSwitchChange" />Menu Split
		</div>

		<div class="ssp-button-view__inputs-cw">
			<el-input v-model="icon"></el-input>
			<el-input v-model="tooltip"></el-input>

		</div>

		<div class="ssp-button-view__inputs-cw">
			<el-select v-model="type" @visible-change="btnTypesSelectorVisibleChange">
				<el-option v-for="item in store.buttonTypes" :key="item" :label="item" :value="item" />
			</el-select>
			<el-select v-model="iconFamily">
				<el-option v-for="item in iconFamilies" :key="item" :label="item" :value="item" />
			</el-select>
		</div>

		<div class="ssp-button-view__buttons-wrapper">
			<SspButton :active="active" :borderless="borderless" :plain="plain" :type="type" :icon="icon" :disabled="disabled"
				:family="iconFamily" :tooltip="tooltip" :menu="store.menu" @click="click"
				@context-menu-item-click="contextMenuItemSelected">
				Default Button
			</SspButton>
		</div>
		<div class="ssp-button-view__other">
			<span>Clicks: {{ clicks }}</span>

		</div>


		<el-button plain :type="type" size="medium">Primary</el-button>
		<el-button :type="type" size="large">Large</el-button>
	</div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { useStore } from '@/store/pinia/index'
import SspButton from "@/components/Generics/Button/index.vue"
import { SspContextMenuItem } from '@/store/pinia/types/ContextMenu';

const store = useStore()
let active = ref(false)
let borderless = ref(false)
let plain = ref(false)
let disabled = ref(false)
let type = ref("default")
let icon = ref("home")
let tooltip = ref("tooltip")
let iconFamily = ref("material")
let iconFamilies = ref(["awesome", "material"])

let clicks = ref(0)

const btnTypesSelectorVisibleChange = (e: boolean) => {
	if (e) {
		store.UpdateData()
	}
}

function OnSplitMenuSwitchChange(v: Event) {
	let checkBox = v.currentTarget as HTMLInputElement
	store.SetSplitMenuValue(checkBox.checked)
}

function contextMenuItemSelected(item: SspContextMenuItem) {
	console.log(item)
}

function click(e:Event){
	clicks.value++
}

//getBtnTypes

</script>

<style lang="scss">
.ssp-button-view__inputs-cw {
	display: flex;
	padding: 8px 0px;
}

.ssp-button-view__inputs-cw>*+* {
	margin-left: 8px;
}

.ssp-button-view__other {
	text-align: left;
	margin-top: 16px;
}
</style>
