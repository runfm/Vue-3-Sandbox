<template>
	<el-tooltip :content="tooltip" :disabled="disabled || !tooltip">
		<a :class="Bem" @click="click" @contextmenu="contextMenu">
			<Icon :icon="icon" :family="family" />
			<div :class="BemTextContent">
				<slot></slot>
			</div>
		</a>
	</el-tooltip>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
export default defineComponent({
	name: "SspButtonBody"
})

</script>

<script lang="ts" setup>
import { computed, toRefs } from 'vue';
import propsObj from "./js/props"
import Icon from "../IconButton/index.vue"

const emits = defineEmits(['click', "contextmenu"])

const props = defineProps(propsObj)

const { icon, family, tooltip } = toRefs(props)
const base = "ssp-button-body"
const Bem = computed<Array<string>>(() => {

	const result = [base]

	if (props.active)
		result.push(`${base}_active`)
	if (props.borderless)
		result.push(`${base}_borderless`)
	if (props.plain)
		result.push(`${base}_plain`)
	if (props.disabled)
		result.push(`${base}_disabled`)
	if (props.menu && props.menu.Split)
		result.push(`${base}_split-menu`)
	/* 	if (props.plain)
			result.push(`${base}_plain`) */

	result.push(`${base}_size_${props.size}`)
	result.push(`${base}_type_${props.type}`)
	return result;
})

const BemTextContent = computed<Array<string>>(() => {
	const textBase = `${base}__tc`
	const result = [textBase, `${textBase}_size_${props.size}`]
	if (props.icon)
		result.push(`${textBase}_icon`)
	return result;
})

function click(e:Event) {
	emits("click",e )
}

function contextMenu(e:Event){
	emits("contextmenu", e)
}

</script>

<style lang="scss">
@import "./style/index.scss"
</style>