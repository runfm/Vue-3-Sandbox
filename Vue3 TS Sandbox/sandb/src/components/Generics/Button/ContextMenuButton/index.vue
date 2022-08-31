
<template>
	<div :class="Bem">
		<div v-for="item in items" :key="item.ID" class="ssp-button-context-menu-item" @click="click(item)">
			{{item.Name}}
		</div>

	</div>

</template>

<script lang="ts">
import { defineComponent, PropType, toRefs } from 'vue';
import { computed } from '@vue/reactivity';

import { SspContextMenuItem } from '@/store/pinia/types/ContextMenu';
export default defineComponent({
	name:"SspButtonContextMenu"
})

</script>

<script lang="ts" setup>

	const emits= defineEmits(["item-click"])

	const props = defineProps({
		items:{
			type: Array as PropType<Array<SspContextMenuItem>>
		},
		size: {
			type: String,
			default: "medium" /* large, medium, small, mini */
  	},
			type:{
			type:String,
			default:"default"
  	}, /* default, primary, success, info, warning, danger */
	})
	const {items, size} = toRefs(props)

	const Bem = computed(()=> {
		const base = "ssp-button-context-menu"
		let result = [base]

		result.push(`${base}__size_${props.size}`)
		result.push(`${base}__type_${props.type}`)

		return result
	})

	function click(item:SspContextMenuItem){
		emits("item-click", item)
	}
</script>

<style lang="scss">
@import "./style/index.scss"
</style>