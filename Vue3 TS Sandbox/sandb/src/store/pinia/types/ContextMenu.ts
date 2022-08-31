export interface SspContextMenuItem{
	ID:string,
	Name:string
}


export interface SspContextMenu {
	Split:Boolean,
	Items: Array<SspContextMenuItem>
}

