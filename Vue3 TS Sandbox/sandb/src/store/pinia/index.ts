import { defineStore } from 'pinia'
import { ref, reactive, computed } from 'vue'
import {SspContextMenuItem, SspContextMenu} from "./types/ContextMenu" 

function createUrl(controller: string, method: string, withHost: boolean = false) {
	let base = process.env.BASE_URL
	let url = `${base}api/${controller}/${method}`
	if (withHost) {
		let host = window.location.host;
		url = host + "/" + url
	}
	url = url.replaceAll(/\/{2,}/gmi, "/")

	return url
}



interface SspState{
	Menu: SspContextMenu,
	ButtonTypes:Array<string>
}



export const useStore = defineStore('counter', () => {
	let sync = ref(false)
	const state_ = reactive<SspState>({
		Menu:{
			Split:false,
			Items:[]
		},
		ButtonTypes:[]
	})
	//let contextMenu__ = reactive()
	let loading = ref(false);

	const buttonTypes = computed(() => state_.ButtonTypes)
	const menu = computed(()=> state_.Menu)

	async function UpdateData() {
		if (sync.value == false) {
			var url = createUrl("newform", "app/data")
			let data = await (await fetch(url)).json()
			
			state_.ButtonTypes = data.ButtonTypes
			state_.Menu = data.ContextMenu
			//sync.value = true
		}

		return state_
	}

	async function SetSplitMenuValue(value:Boolean){
		state_.Menu.Split = value
		return state_
	}

	return { loading, buttonTypes, menu, UpdateData, SetSplitMenuValue }
})
